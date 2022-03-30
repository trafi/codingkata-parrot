using System.Collections.Concurrent;

namespace CodingKata;

public class InMemoryParrotCache : IParrotCache, IDisposable
{
    private readonly SemaphoreSlim _semaphore;
    private readonly ConcurrentDictionary<string, ParrotCacheEntry> _cache;
    private readonly ConcurrentQueue<ParrotCacheEntry> _cleanUpQueue;
    private readonly TimeSpan _timeToLive;
    private readonly Timer _timer;

    public InMemoryParrotCache(TimeSpan timeToLive)
        : this(timeToLive, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))
    {
    }

    public InMemoryParrotCache(TimeSpan timeToLive, TimeSpan cleanupDelay, TimeSpan cleanupPeriod)
    {
        _timeToLive = timeToLive;
        _timer = new Timer(CleanUp, default, cleanupDelay, cleanupDelay);

        _semaphore = new SemaphoreSlim(1, 1);
        _cache = new();
        _cleanUpQueue = new();
    }

    public async Task<IParrot> GetOrAddParrot(string parrotId, Func<Task<IParrot>> factory)
    {
        // use cache date time now so it won't change during method execution
        var now = DateTimeOffset.UtcNow;

        // first check if cache contains item and if item is not expired
        if (_cache.TryGetValue(parrotId, out var cacheEntry) && now <= cacheEntry.ExpiresAt)  
            return cacheEntry.Parrot;

        try
        {
            // lock expensive part. classical `lock` statement and ReaderWriterLockSlim does not work in async context
            await _semaphore.WaitAsync();

            // check again if cache has entry
            // this can happen in rare cases were multiple threads invokes this method with same cache key
            // you can try to comment those lines and run unit tests to see described behaviour
            if (_cache.TryGetValue(parrotId, out cacheEntry) && now <= cacheEntry.ExpiresAt)
                return cacheEntry.Parrot;

            // invoke expensive factory method
            var newParrot = await factory();
            var newCacheEntry = new ParrotCacheEntry
            {
                Parrot = newParrot,
                ParrotId = parrotId,
                ExpiresAt = now.Add(_timeToLive)
            };
            // add cache entry into clean up queue
            // this is required to cleanup stale elements from cache and keeping memory usage at the minimum
            _cleanUpQueue.Enqueue(newCacheEntry);
            
            // add new entry into cache regardless if entry already exists and return
            return _cache.AddOrUpdate(parrotId, (_) => newCacheEntry, (_, _) => newCacheEntry).Parrot;
        }
        finally
        {
            // release lock even if exception was thrown
            _semaphore.Release();
        }
    }

    // dispose is a must because:
    // timer has some internal state to cleanup 
    // semaphore has some unmanaged resources to free
    public void Dispose()
    {
        _timer.Dispose();
        _semaphore.Dispose();
    }

    // point to clarify with interview why async void is bad and why it exist?
    private async void CleanUp(object? timerState)
    {
        // use cache date time now so it won't change during method execution
        var now = DateTimeOffset.UtcNow;

        // keep peeking into queue items for all expired cache entries
        while (_cleanUpQueue.TryPeek(out var queueEntry) && now > queueEntry.ExpiresAt)
        {
            if (!_cleanUpQueue.TryDequeue(out queueEntry))
                break;

            try
            {
                // acquire lock just incase another thread is currently adding new cache entry with same key
                await _semaphore.WaitAsync();

                // just double check if cache entry is expired
                // can happen in between timer calls that value was expired and new value retrieved from factory
                // in the end cleanup queue would end up with multiple items with same key
                if (_cache.TryGetValue(queueEntry.ParrotId, out var cacheEntry) && now > cacheEntry.ExpiresAt)
                    _cache.Remove(queueEntry.ParrotId, out _);
            }
            finally
            {
                // release lock even if exception was thrown
                _semaphore.Release();
            }
        }
    }
}

class ParrotCacheEntry
{
    public IParrot Parrot { get; set; }
    public string ParrotId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}
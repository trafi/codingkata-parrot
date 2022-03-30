# Coding Kata pairing exercise

## How to start
- First things first this exercise should take no longer than 1.5h
- Introduce yourself and exercise
- Explain your expectations and what is your plan
    - first part refactoring existing code to something more usable
    - implementing new features
- Use Visual Studio Code + Live Share extension
- Ideally refactoring should take no more than less of your allocated time. If you see that you won't fit into your timeline achieve some stable version of code (project builds and tests passes) and move onto the next part.

> Double check if vscode + Live Share works before you start


## Refactoring Parrot - 1st part

Ask what candidate what he sees wrong and start from that. Tips if things got stucks:
1. can something be simplified
1. can something be unified (naming, style, etc)
1. can business logic be extracted into smaller logic units (class per parrot type etc)
1. what does magic numbers mean
1. how does static ctor work, can we remove it

> Do not forget to run tests from time to time. Do they pass? Business logic should not be changed after refactoring.


## Implementing caching - 2nd part

With caching implementation start small and make in more complex as you go
1. Implement cache just to get or add items into some collection. If collection choice has poor performance (lets say it is an array) ask how we can improve it, what would happen if we will have more items than array size, etc
2. Ask to make memory cache with some expiration logic. How can we configure time to live
3. Ask to make algorithm thread safe. Why concurrent dictionary is not enough? What we can use in async context if lock does not work?
If everything is inside lock can we make it more performant? How?
4. Ask how we can ensure that all stale items eventually will be removed from cache (and RAM). Why would we want this? How can we achieve it?

> After each step new unit test should be added.
> If you are running out of time explain that we probably want finish everything and you would rather ask some more questions without need of any code. Then go through all uncovered parts.

### Solved exercise

You can reference solved exercise to get some ideas on how things could be implemented. `solved-tautvydas` also contains some comments in source code you can read to get the idea on how and why.

> Everyone is welcome to provide their solution in this repo!
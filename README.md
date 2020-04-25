# OFX Battleship State Tracker
This repo is a coding test for [OFX](https://www.ofx.com), with an implementation of a Battleship state tracker API.

## The Task
The task is to implement a Battleship state tracking API for a single player that must support the following logic:

- Create a board
- Add a battleship to the board
- Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.

The API should not support the entire game, just the state tracker. No graphical interface or persistence layer is required.

## Overview
Vertical Slice implementation of Battleship, details coming soon...

Unit and Integration testing has been added for:

- Commands
- Command Validation
- Controllers

## Deployment
The application has been deployed to AWS and is available here:  
Swagger / OpenAPI docs available here:

## References
I made use of several libraries to aid clean architecture:

- Mediatr: https://github.com/jbogard/MediatR
- AutoMapper: https://automapper.org/
- Fluent Validation https://fluentvalidation.net/
- Fluent Assertions: https://fluentassertions.com/introduction

## ToDo - Playable Game
Features to add with the aim of a playable game:

- Game Player construct
  - associative between game and player
  - winner = p1/p2
- Game State
  - player one
  - player two
  - setup -> until all ships placed
  - in progress
  - gameover -> set winner/loser
- Player ship inventory, number ot different size ships to add to board
  - pre-defined ship names/sizes
- Shot concept
  [x] validate, who's turn
  [ ] validate current game
  [x] hit -> update ship part
  [x] record shot
  [ ] check gameover -> update game state

## Rules of the Game

Each player has 5 ships:
- Carrier, which has five holes
- Battleship, which has four holes
- Cruiser, which has three holes
- Submarine, which has three holes
- Destroyer, which has two holes

Reference: https://www.thesprucecrafts.com/the-basic-rules-of-battleship-411069

## ToDo - Other

- AutoMapper Configuration Tests
- Try out Respawn: https://github.com/jbogard/Respawn

## Future
Some other things to play with in future:

- Remodel resources: https://www.thoughtworks.com/insights/blog/rest-api-design-resource-modeling
- Pages front end
- SignalR for multi-player

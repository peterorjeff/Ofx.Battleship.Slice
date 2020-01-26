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

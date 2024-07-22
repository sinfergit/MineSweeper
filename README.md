# Minesweeper

## Overview
This repository contains the implementation of a Mine Sweeper game built using .NET 7. The design of the game is inspired by Clean Architecture, Object-Oriented Programming (OOP), and SOLID design principles. The primary focus of the design is to ensure code reusability, clarity, and testability.

## Features

Customizable Grid Size: Users can specify the dimensions of the grid.
Dynamic Mine Placement: Users can define the number of mines to be placed on the grid.

## Tools and Libraries

.NET 7: Framework used for building the game.
xUnit: Testing framework used for unit testing to ensure code reliability and correctness.
Serilog: Logging library used to track and record application events and errors.

## Testing

To run unit tests, use the following command:

sh
Copy code
dotnet test
Logging

Logs are managed using Serilog and are configured to output to various sinks as specified in the Serilog configuration.

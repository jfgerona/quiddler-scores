# Quiddler Scores

A C#/.NET 5 project that implements game services for the card game **Quiddler** through a reusable class library and a console client.

## About the Project

This project was created for **INFO-5060**. The goal was to build a **.NET 5 class library** that provides core Quiddler-related services, along with a **console client** that demonstrates those services through user interaction.

The library is responsible for managing the deck, players, card handling, and word scoring. The client handles user input and displays the results in the console.

## Features

- Built with **C#** and **.NET 5**
- Uses a **class library + console client** structure
- Exposes functionality through the `IDeck` and `IPlayer` interfaces
- Supports:
  - creating a deck
  - creating players
  - dealing cards
  - drawing cards
  - discarding cards
  - picking up the top discard
  - testing words for validity and score
  - playing words and tracking total points
- Uses **Microsoft Word Object Library** spell checking to validate candidate words
- Demonstrates interface-based design and component interaction

## Project Structure

```text
QuiddlerScores/
├── QuiddlerLibrary/     # Class library containing deck/player logic
├── QuiddlerClient/      # Console application demonstrating the library
└── README.md

# README.md — Rhythm Game

## Project Overview
- This project consists of a 2D rhythm game where the player needs to press the required input according to the music beat.

---

## Game Scenes
 - The first scene "TitleScene" is the title screen waiting for the player to start the game.
 - The second scene "GameplayScene" is the gameplay phase.
 - The third scene "BoardScene" is the final scene where the scoreboard is shown.

---

## Architecture (MVP)

| Layer | Namespace | Responsibility |
|-------|-----------|---------------|
| Domain (Model) | `RythmGame.Domain` | Pure C#. No MonoBehaviour. Logic only |
| View | `RythmGame.View` | MonoBehaviour. UI updates and input only |
| Presenter | `RythmGame.Presenter` | Connects Model ↔ View |

---
## Design Patterns (State)

The state design pattern is used in the GameplayScene. There are 3 states in total:
1. IntroState: While the note reaches the rhythm timing line.
2. PlayingState: The gameplay loop.
3. GameoverState: Shows the final score of the player.


## Design Patterns (Observer Pattern)

With the observer pattern, an object, known as the subject, keeps a list of dependents, known as the observers. When something happens in the game that you need other objects to know about, your subject object can invoke a function that the observers all subscribe to.

## Conductor Class

The Conductor class is the song managing class that the rest of the rhythm game will be built on. This class tracks the song position, and controls any other synced actions. 

It must contain the following variables:

//Song beats per minute determined by the song you're trying to sync up to
public float songBpm;
//The number of seconds for each song beat
public float secPerBeat;
//Current song position, in seconds
public float songPosition;
//Current song position, in beats
public float songPositionInBeats;
//How many seconds have passed since the song started
public float dspSongTime;

## Coding Conventions

Constants / static readonly : UPPER_SNAKE_CASE
Class names / public methods : PascalCase
Private fields               : _camelCase
[SerializeField]             : private _camelCase


**Prohibited:**
- `MonoBehaviour` inheritance in Domain layer
- `using UnityEngine;` in Domain layer

---

## Directory Structure

Assets/_Project/Scripts/
├── Domain/         # Pure C# — zero Unity dependency
├── View/           # MonoBehaviour
├── Presenter/      # 

---

## Do NOT
- Change the existing architecture without explicit approval
- Use ScriptableObject as a runtime data store
- Manipulate Unity types (e.g. Vector3) directly inside Domain layer

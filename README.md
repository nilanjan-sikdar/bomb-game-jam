# OH HO, I SEE SOMEONE!

> A Cyber-Neon Arcade Stealth Runner built with Unity 2D.

![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)
![License](https://img.shields.io/badge/License-MIT-green.svg)
![Status](https://img.shields.io/badge/Status-Prototype-orange.svg)

## 📖 Table of Contents
- [About the Game](#about-the-game)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [How to Play](#how-to-play)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)
- [Credits](#credits)

## 🎮 About the Game
**OH HO, I SEE SOMEONE!** is a 2D side-scrolling arcade game developed during a 12-hour prototype challenge. The game combines platforming mechanics with "Red Light/Green Light" stealth elements.

**Premise:** A defeated boxer enters a cosmic game show where he must navigate neon alleyways while avoiding the gaze of a giant, sweeping Eye.

## ✨ Features
- **Movement System:** Custom physics-based character controller (Left/Right movement + Single Jump).
- **Stealth Mechanic:** Raycast-based detection system from the "Eye" enemy.
- **Dynamic Animation:** State-machine implementation for Idle, Run, and Jump transitions.
- **Score System:** Survival-time based scoring.

## 🚀 Getting Started

To run this project locally, follow these steps.

### Prerequisites
* **Unity Hub**
* **Unity Editor** (Version 6.2(6000.2.14f1)
* **Visual Studio** (for script editing)

### Installation
1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/nilanjan-sikdar/bomb-game-jam.git](https://github.com/nilanjan-sikdar/bomb-game-jam.git)
    ```
2.  **Open in Unity:**
    * Open Unity Hub.
    * Click `Add` -> `Add project from disk`.
    * Select the cloned folder.
3.  **Load the Scene:**
    * Navigate to `Assets/Scenes`.
    * Double-click `MainLevel.unity`.

## 🕹️ How to Play

The goal is to survive as long as possible without being detected by the Spotlight.

| Key | Action |
| :--- | :--- |
| **A / D** or **Left / Right** | Move Player |
| **Space** | Jump |
| **R** | Restart Game |
| **P** | Pause Game |

**Rule:** When the spotlight is ON you, stop moving immediately. If your velocity is > 0 while in the light, it is Game Over.

## 📂 Project Structure

```text
Assets/
├── Animations/       # Animator Controllers and Animation Clips (.anim)
├── Art/
│   ├── Sprites/      # Character and Environment sprites
│   └── Backgrounds/  # Parallax background layers
├── Scripts/
│   ├── PlayerMovement.cs   # Handling Rigidbody2D and Input
│   ├── EyeController.cs    # Logic for the spotlight rotation
│   └── GameManager.cs      # Score tracking and Restart logic
├── Scenes/           # MainLevel and MainMenu
└── Prefabs/          # Player and Obstacle prefabs

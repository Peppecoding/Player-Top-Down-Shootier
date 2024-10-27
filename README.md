# Player Movement & Aiming System in Unity

This repository contains a **Unity C# script** that implements a **third-person player movement and aiming system** with **animations**. The script allows the player to walk, run, aim, and shoot, providing smooth and responsive controls for a realistic gameplay experience.

## Features

- **Movement Modes**: Walk and run modes based on user input, with a configurable walk and run speed.
- **Aiming System**: Uses raycasting for aiming at a target within a specified layer mask, with an aim indicator that follows the cursor.
- **Shooting Mechanism**: Triggers a shooting animation when firing.
- **Animator Integration**: Smooth transitions between idle, walk, and run animations, based on movement and aiming input.
- **Character Controller**: Utilizes Unity’s Character Controller for smoother and more customizable movement.
- **Gravity Simulation**: Adds realistic gravity behavior for grounded and airborne states.

## Script Overview

### 1. Movement
   - Configures **walk** and **run** speeds and adjusts based on player input.
   - Handles **rotation** towards the aiming point for a more immersive third-person experience.

### 2. Aiming System
   - **Raycasting** calculates the target point from the camera to an object within a specified layer mask.
   - Activates an **aim indicator** to visually show where the player is aiming.

### 3. Shooting Mechanism
   - Triggers a **"Fire"** animation when the player initiates the shoot command.

### 4. Animator Controllers
   - Syncs movement animations with player actions, including **walking**, **running**, and **aiming**.

## How to Use

1. **Add the Script** to your player character GameObject in Unity.
2. **Assign the Character Controller** component, animator, and aim indicator in the Unity Inspector.
3. **Configure Inputs** using Unity's Input System and map the controls accordingly.
4. **Set Parameters** in the Inspector (walk speed, run speed, aim layer mask) for customization.

### Input Requirements

- **Move**: Map to a joystick or WASD keys.
- **Run**: Map to a toggle (e.g., Shift key) to switch between walk and run modes.
- **Aim**: Enable the aim indicator by moving the mouse or thumbstick.
- **Fire**: Map to a button (e.g., left mouse button) to trigger the shooting animation.

## Dependencies

- Unity’s **Character Controller** and **Animator** component.
- **Unity Input System** for handling input mappings.

---

## License

This project is open-source and available for personal and educational use. Contributions and feedback are welcome!

---

## Demo

Check out the video demo on [YouTube Shorts](https://www.youtube.com/shorts/TyoAd8QJd30) or [Instagram](https://www.instagram.com/p/DBowjrNuTpA/?hl=en) to see this movement and aiming system in action.

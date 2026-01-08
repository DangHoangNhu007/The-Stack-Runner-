# 🏃‍♂️ The Stack Runner - Hypercasual

> A fast-paced hypercasual runner game built with Unity, featuring stacking mechanics, swerve controls, and procedural level generation.

![Image](https://github.com/user-attachments/assets/e2f557b3-2c10-40e3-9d36-b7b148af5d9c)

## 🎮 About The Project
This project serves as a technical demonstration of **core hypercasual mechanics**. The goal is to navigate a character through obstacles, stacking up blocks to increase height and survive wall collisions.

The project focuses on **smooth input handling**, **game loop architecture**, and **visual polish (Game Feel)**.

## 🛠 Tech Stack
*   **Engine:** Unity 6.3(LTS)
*   **Language:** C#
*   **Plugins:** DOTween (for smooth animations)
*   **Architecture:** Singleton Pattern (GameManager), Component-based design.

## 🚀 Key Technical Features

### 1. Swerve Input System
*   Implemented a smooth touch/mouse control system using `Mathf.Clamp` to keep the player within screen bounds.
*   Normalized input sensitivity for consistent behavior across different screen resolutions.

### 2. Stacking Logic (Data Structures)
*   Managed the stack using `List<Transform>` for dynamic adding/removing of objects.
*   Implemented **Parent-Child hierarchy manipulation** to ensure the stack moves cohesively with the player.
*   Added **Physics-based interactions**: When a brick is lost, it detaches, regains rigidbody physics, and is pushed back for a realistic effect.

### 3. Custom Editor Tool (Productivity)
*   Developed a custom **Level Generator Tool** (`EditorWindow`) to procedurally spawn obstacles and pickups.
*   Features: Customizable path length, obstacle density, and prefab randomization.

### 4. Game Feel ("The Juice")
*   **DOTween Integration:** Utilized `DoPunchScale` and `DoJump` for satisfying collecting feedback.
*   **VFX:** Custom particle systems for shattering effects and collisions.
*   **Camera Shake:** Dynamic camera trauma applied upon player death/collision.

## 🕹️ How to Play
*   **Hold & Drag:** Move Left/Right.
*   **Collect** yellow cubes to grow taller.
*   **Avoid** red walls (or sacrifice cubes to break them).
*   **Reach** the finish line to win!

---
**Developed by [Dang Hoang Nhu]**
*   [https://www.linkedin.com/in/%C4%91%E1%BA%B7ng-nhu-257b09317/]*
![Image](https://github.com/user-attachments/assets/c2e7ac60-22f3-4969-9307-57f0025cbdb1)

![Demo Gameplay](./Media/animation-2.gif)

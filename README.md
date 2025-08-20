**Cube Rush**
\[Gameplay Link: [https://avinashsingh.itch.io/cube-rush](https://avinashsingh.itch.io/cube-rush)]
*Note: For optimal input support, please play on a mobile web browser. Some desktop browsers may not fully support input controls.*

**Overview:**
*Cube Rush* is an action-packed arcade-style mobile game developed using Unity. The game emphasizes performance optimization and advanced gameplay mechanics, including custom systems for animation, collision, and AI behavior.

**Key Features and Technical Highlights:**

* **Collectible Generation:**

  * Implemented efficient collectible spawning using GPU instancing to optimize performance.
  * Custom animation system developed to support GPU-instanced objects, as Unity does not support default animations in this context.
  * Prefabs were deliberately avoided to maintain performance and flexibility.

* **Custom Collision System:**

  * Designed and implemented a uniform grid-based collision detection system for accurate and efficient interaction handling.

* **Player Movement:**

  * Integrated an on-screen joystick control system for intuitive mobile player input and movement handling.

* **Bot AI System:**

  * Developed AI using a finite state machine architecture implemented via ScriptableObjects.
  * Added context steering to enable bots to mimic dynamic player-like movement and decision-making.

* **Cannon Reload System:**

  * Player's cannon includes a reloading mechanic with cooldown times that scale based on the player's current level to balance gameplay difficulty.

* **Particle Systems:**

  * Designed and integrated multiple custom particle effects to enhance visual feedback and overall game aesthetics.

\# 💣 Cannon Carnage



A 2D physics-based action game built with \*\*Unity\*\* and \*\*C#\*\*, with touch controls for mobile and keyboard controls for web. Developed during my game development internship.



\## 🎮 Play the Game



\*\*\[▶ Play Cannon Carnage Online](https://portal.uetgamestudio.com/games/cannoncarnage)\*\*



No download needed. It runs in your browser on desktop and mobile.



\## 📌 About the Game



You control a cannon and must stop falling rocks and blades from hitting it. Enemies keep bouncing around until you destroy them with your cannonballs, and the round is lost if the cannon is hit. The game includes power-ups, upgrades, and a challenge that increases as you progress through the levels.



\## 🎯 Features



\- \*\*Cannon combat:\*\* shoot cannonballs to destroy falling rocks and blades

\- \*\*Physics-based enemies:\*\* rocks bounce continuously, and blades slide across the ground after landing until they hit a wall and explode

\- \*\*Explosions and loot:\*\* destroyed enemies trigger an explosion effect and drop a random number of diamonds that scatter with physics impulses

\- \*\*Win and lose conditions:\*\* the level is won once all enemies are destroyed, and a delayed Game Over screen appears when the cannon is hit

\- \*\*Multiple levels\*\* with a "Next Level" flow after winning

\- \*\*Main menu\*\* with an animated instructions panel and a quit confirmation popup

\- \*\*Pause menu\*\* with Resume, Restart, and Home options

\- \*\*Animated UI panels\*\* that scale in and out smoothly, even while the game is paused

\- \*\*Sound effects and audio manager\*\* for gameplay feedback

\- \*\*Power-ups and upgrades\*\* with increasing difficulty

\- \*\*Platform-aware controls:\*\* on WebGL, the game detects whether the player is on a mobile device and switches between on-screen touch controls and web controls (including a different wall layout)



\## 🧩 Key Scripts



| Script | What it does |

|---|---|

| `baldesmovement.cs` | Blade enemy behavior: ground detection, horizontal movement, wall collision, explosion, diamond drops |

| `WinCondition.cs` | Tracks destroyed enemies and shows the animated win panel |

| `GAMEmanager.cs` / `GAMEOVER.cs` | Delayed game over, panel animation, restart, home, next level |

| `Puasemanu.cs` | Pause, resume, restart, and return to the main menu |

| `Mainmenuscript.cs` | Main menu flow, instructions panel, quit popup, level loading |

| `CanvasManager1.cs` / `WallsManager.cs` | Mobile vs. web detection that switches the UI and walls |



\## 🛠️ Built With



\- Unity (2D)

\- C#

\- WebGL

\- Git \& GitHub



\## 📂 Project Structure



```

Cannon-Carnage/

├── Assets/           # Scenes, scripts, sprites, audio, prefabs

│   ├── audio/

│   ├── blades/

│   ├── cannon/

│   ├── cannonball/

│   ├── Explosion/

│   ├── GAME MANAGER/

│   └── rocks/

├── Packages/         # Unity package configuration

└── ProjectSettings/  # Unity project configuration

```



\## ▶ How to Open the Project



1\. Install Unity Hub and the Unity version listed in `ProjectSettings/ProjectVersion.txt`.

2\. Clone the repository:

```

&#x20;  git clone https://github.com/Mtaha-az/Cannon-Carnage.git

```

3\. Add the folder in Unity Hub and open it.

4\. Open the \*\*Main Menu\*\* scene from `Assets` and press \*\*Play\*\*.



\## 👨‍💻 My Role



Developed during my game development internship at UET Game Studio. \*(Edit this line to say exactly what you did, for example gameplay scripting, UI, or level design.)\*



\## 📫 Contact



\[GitHub: Mtaha-az](https://github.com/Mtaha-az)


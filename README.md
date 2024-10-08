<h1> Initial G </h1>

Initial G is my first project developed using Unity and C# after completing several courses and tutorials. This project is a hands-on exploration of game development, where I am applying what I have learned about programming, design principles, and the overall development workflow. While there is still room for improvement, it is a meaningful step toward expanding my skills and experience in game development.

<h3>Core Gameplay Summary</h3>
For those unfamiliar with Vampire Survivors, Initial G is a basic survival roguelike game where players must survive through endless waves of enemies. 
Defeating enemies grants experience points, and leveling up offers access to new items or upgrades for currently equipped gear.

<h3>How to run</h3>
The game is currently available to run through the Unity editor. A stable version is located on the Main branch. To run the game, open the project in Unity and start the app in MainMenu scene.</br>
Supports keyboard and gamepad.
<h5>Controls</h5>
- Navigation: WASD/Analog</br>
- Run: Shift/L2</br>
- Confirm: Enter/Button South</br>
- Deny: Esc/Button East</br>

<h2>Development Approach</h2>
<p>While working on Initial G, I wanted to keep things flexible and follow some common practices from the game development industry. Even though I was the only one developing the game, I treated it like a collaborative project, focusing on features that would make life easier for level and game designers. I tried to ensure that future expansions and additions could be accommodated by maintaining a flexible architecture, allowing for new features and enhancements without major overhauls.</p>

<h2>Key Feautres</h2>
In this section, I highlight some of the more interesting features of the project. These elements showcase the technical aspects and design choices that enhance both functionality and performance.

<h4>Configurable Game Elements</h4>
<p>Some of the features that offer designers a range of options to tweak gameplay and introduce variation:  </p>
- Customizable items and unlock conditions</br>
![image](https://github.com/user-attachments/assets/aab1b9b7-b9e5-441a-88e9-a8e6c83c7e1e)
![image](https://github.com/user-attachments/assets/2c8cd47c-80c8-4c11-a1a7-e5b1404f76b3)</br>
- Customizable level settings including enemy waves management </br>
![image](https://github.com/user-attachments/assets/ee1ac157-4d42-4587-9fcb-53551f69b443)</br>
- Customizable loot control </br>
![image](https://github.com/user-attachments/assets/bedbcc6d-5bd5-41dc-99c9-0626369101d7)
![image](https://github.com/user-attachments/assets/a5943281-d9cd-4057-ad96-136e1bec998a)

<h4>Dynamic Object pooling</h4>
Utilizes dynamic object pooling, which automatically manages and recycles objects based on gameplay requirements. All object creation, instantiation, and removal are handled through this pooling system to maximize efficiency. To further enhance performance, some object pools are preloaded during the loading screen.

<h4>Map control and generation</h4>
Key components include effective map controller that dynamically manages four instances of the provided map on a virtual grid, creating the illusion of an 'endless map.' Paired with object pooling, it tracks the location of all loot and treasures, respawning them as needed when player returns to previously visited areas.

<h4>Save system & Profile management</h4>
The game includes a save system with profile management, allowing players to create, delete, and rename profiles. Game settings are stored separately from profiles, while individual progress is saved for each.

<h4>Custom enemy pathfinding</h4>
Enemy movement controller allows them to navigate around obstacles as well as avoid other enemies to prevent stacking.

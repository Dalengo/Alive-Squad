# Alive Squad

Back to the surface, Alive Squad immerses you in a universe mixing adventure and mystery, the goal: to find your way through the bowels of Zuri, a hostile planet populated by extraterrestrials.
    
To do this, you will have to understand and adapt to your environment in order to survive! To win, you have to climb the floors by jumping with precision while staying alive.

Alive Squad is developed by Clément Cotte, Matis Lima Barros, Ange Mercoyrol--Dol and Yanis Martin.

## Introduction

### Story

The story begins when a group of astronauts leave the overpopulated Earth in order to find a new habitable planet. Their ship being damaged by a storm of asteroids, they crash on a planet, Zuri. But when trying to get out of the ship, they slip to the bottom of a crevasse. The majority of the crew gets captured by a mysterious species living on this planet. 

Alone, Philippe avoids being captured but found at the bottom of a cave. His goal is simple, free his comrades and find a way to repair the ship to return to Earth.

    
### Gameplay 

It is a platform game where the character must arrive at the end of each
level by jumping from platform to platform as quickly as possible. The objective will be
to go up from floor to floor until the arrival. The player will interact with his environment,
and will for example have to avoid being detected by enemies. The games will be
in the form of levels, which will be unlockable by completing the previous level. 

Each level will have its particularity and will be unique. The game will get harder and harder level by
level. The player will discover the mechanics of the game as they go. You will have to be precise
and fast while on the move so you don't fall off and get lost.

### Game Style
Alive Squad is inspired by the famous video games Doodle Jump, Getting
Over It or Jump King. These three games have one thing in common, like Alive Squad, they have a
easy to understand concept however Doodle Jump is a game rather made for parties
fast and is easy unlike the other two which are complicated games, frustrating and
long. Alive Squad is a mix of both.

<p align="center">
  <img src="/Alive-Squad_Website/Images/Doodle-Jump-2068.png" width="300" height="300"> <img src="/Alive-Squad_Website/Images/jump-king-9373-6874.png" width="500" height="300">
  <img src="/Alive-Squad_Website/Images/getting-over-it.png" width="500" height="300">
</p>
 
## First soutenance 

### Progression 03/11/2022
    
The camera has been implemented and follow the character. Elements and collision boxes needed for levels have been created. The character and his movement animations have been set up. The main menu and the pause menu have also been implemented.

### Camera
The camera follows the character with the CameraFollow script. For this we tell it at each frame to move towards the Player adding 2/10 of a second of delay for
visual comfort.    
<p align="center">
  <img src="/Alive-Squad/Alive-Squad_Website/Images/Game.png" width="900" height="300"> 
</p>
    
### Modeling
Tilesets were imported and then cut into blocks to build the levels. Then a test platform was created to test the character's movements.
character. We then found graphics of astronaut video games on the internet on the internet that we used on the character. Then we managed the
Then we used the sprites to create animations and link them together.
<p align="center">
  <img src="/Alive-Squad_Website/Images/palette.png" width="570" height="430"> 
</p>
     
### Main menu
Main menu allows to quit the game, access to the settings (for the moment "FullScreen" and "Sound" are the two available settings), and allows you to choose your level by accessing the level menu (level 1 at the moment).
<p align="center">
  <img src="/Alive-Squad_Website/Images/Menu_principale.PNG" width="750" height="500"> 
</p>
    
### Pause menu
Pause menu has 2 buttons "Resume" and "Main, the first one removes the pause and the game continue; the second one returns to the main menu and leaves the game in
progress. It is also possible to access and exit the pause menu with the Escape key.
<p align="center">
  <img src="/Alive-Squad_Website/Images/menu_pause.PNG" width="500" height="280"> 
</p>
    
### Forecast for the future
We will set up the first enemies and the end zones of the game
in order to move on to the next levels. We will make sure that all the levels are finished by the next
submission so that we can focus on multiplayer and the infinite path as well as refinements.

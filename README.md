# Little Warrior

Welcome to Little Warrior, a thrilling adventure game where you, as the courageous "Little Warrior," embark on a perilous journey through treacherous lands filled with witches, spike traps, and relentless enemies. Your mission? To reach the end of each level intact, collect the people's gold, and emerge victorious against all odds and protect your village.

## Gameplay Overview

In Little Warrior, players take on the role of a brave hero navigating various levels fraught with dangers. Your primary objectives include:

Survival: Navigate through each level while dodging spike traps strategically placed by cunning enemies.

Combat: Engage in battles with witches and other adversaries who stand in your way. Utilize your skills to defeat them and progress.

Collecting Gold: Gather the people's gold scattered across the levels

## Key Technical Features

The Character model contains two seperate colliders a boxcollider2D and a circleCollider2D, the circle collider is placed on the lower half of the character model
to allow for smoother movement and reduce the ammount of times the collider could get caught on surrounding colliders, the boxcollider2D being placed on the top half simply 
because it fits the character model better.

Character Attacking, when the Character attacks a polygon collider is enabled in front of the player shaped like the swing of the sword in the animation played (triangle), allowing for the attacking collider to make contact with an enemyies collders (constructed the same as the player), reducing enemy health from 12 to 6. (reasoning for such high health on the enemies)was to allow for other forms of damage to occur to the enemy other than the player, i.e. falldamage, spikes, character abilites, falling off map.

Some features needed to be timed such as when an enemy dies, so animation events were added to allow for timed actions such as destroying the game object on animation compelation. For enemies their attacks were timed using animation events to spawn their projectiles at the correct time when being cast, the projectile then exploads within a given time or unless it collides with the colliderstagged Player or Ground. where as when the player interacts with spikes since there is no animation for them the solution as to simply disable the collider for 2 seconds to allow the player to move from the location otherwise the collder would instantly kill the player as too many collisions would be registed is such as small time.

## How to Play

### Controls: 

Objective: Reach the end of each level while overcoming traps and defeating enemies.

### Keybord Movement

Move Left/Right - A/D                                                                                                                                                            
Jump - Spacebar                                                                                                                                                                  
Attack - Mouse Button 0                                                                                                                                                          
Interact - E

### Xbox Controller Movement (Menu Controll Not Supported)

Move Left/Right - Left Joystick                                                                                                                                                 
Jump - A                                                                                                                                                                        
Attack - X                                                                                                                                                                       
Interact - Y                                                                                                                                                                    

Collectibles: Gather as much gold as you can along the way

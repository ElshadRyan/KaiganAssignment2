<h1>How To Play</h1>

W, A, S, D to move the camera around<br>
left mouse button to click the NPC to be able to change the outfit

<h1>Required Question</h1>
1. Your design Decision and assumption?<br>
2. What rendering optimization you use? <br>
3. Unity Profiler Results(screenshoot/Summary) <br>
4. What you would add with more time? <br>
   
<h1>Bonus Question</h1>
1. How would you profile and optimize this for low-end mobile devices?<br>
2. if you were building this as part of a larger system, what kind of tooling or pipelines would you create for designers?

<h1>Answer</h1>
1. I decide to made the system for choosing skins using both the skinnedMeshRenderer and GameObject because that is the initial setup that i got, 
and i assume if i change the setup too much, it will affect the entire project when working with teams.<br><br>
2. I use combining mesh as my approach to optimize the game, because i think it is the best approach to optimize the game.
GPU instancing required the mesh to have the same kind of materials, and it will be painfull since i instantiate the NPC with randomize outfit
as for Atlasing, i want to try it, but sadly i runout of time<br><br>
3. I benchmark the game by instancing 1000 NPCs into the scene. eventhough i already implemented the mesh combining technique, it doesn't change much since there are still at least 1000 mesh in the scene.
With the combining mesh technique, i get around 35-40 FPS :) from 15-20 FPS. I combine all the NPC's outfit and NPC's Body into 1 single mesh. that means 1 NPC have 1 single mesh.<br><br>

<h2>before</h2>
<img width="1036" height="581" alt="image" src="https://github.com/user-attachments/assets/0375545f-b384-4c79-a5de-7f5cd6363444" />
<img width="975" height="562" alt="image" src="https://github.com/user-attachments/assets/68e166c3-c738-4145-a09e-eac99d89808a" />

<h2>after</h2>
<img width="975" height="561" alt="image" src="https://github.com/user-attachments/assets/44e66cc2-c845-498b-8ffa-7e4774328a68" />
<img width="975" height="550" alt="image" src="https://github.com/user-attachments/assets/3701d117-08c0-494e-a6cb-b8736bcc6186" /><br><br>
4. 1st, i will implement the material attlasing optimization technique, since my optimization still bad. 
than i want the player to be able to see the preview when changing the outfit, like in character customization

<H1>Bonus Answer</H1>
1. for low-end mobile device, i will implement 2 other optimization method that is Occlusion culling, and LOD system.
i think both of that method for reducing drawcall is the best aproach in this scenario.
as for Code optimization, i think the best way is to reduce the amount of memory that it reduce the CPU's work load<br><br>
2. i will create a pipeline for designers to be able to add more categories easily for example: adding wings, or adding weapons.
because i only made so that the designers can add more parts inside the existing categories exaple: adding more clothes, adding more pants

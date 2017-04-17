# Code-Kata-Challenge
This procedurally generates and names a new item with attributes for an RPG game
Done in C# and Unity 3D 5.5.3

ItemGeneratorScript.cs is meant for the player and instantiates a new item when Space is pressed. 

ItemAttributes.cs initializes the object and goes through a procedure to set its values, name it, and sends the info to the UI setting script

UISetter.cs takes the variables form ItemAttributes.cs and sets them as text in a Unity canvas to see in game. 

Web build available here : https://zachwinegardner.itch.io/item-generator-web

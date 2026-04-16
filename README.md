### Item Shop with Stat Effects

##### 

##### General Overview



This project features:

* Item shop with money
* Inventory to hold the items
* Equipment section of the inventory, to track equipped items
* Player stats which are affected by the equipment



##### Project Structure



* Assets/ScriptableObjects: Holds custom SO's that define game data and settings.

  * /Items: In-game items that can be bought from the shop
  * Settings that define stat type names and base values
* Assets/Prefabs: Useful prefabs for objects that appear many times in one scene potentially in more scenes

  * /Managers: Prefabs for persistent managers
  * /UI: UI objects such as inventory items that are instantiated at run time
* Assets/Input: Input settings
* Assets/Scenes: The only scene
* Assets/Textures: Icon textures
* Assets/Enums: enumerations for item types, stat types, and inventory types
* Assets/Scripts: Gameplay scripts

  * /Manager: Persistent singletons
  * /CharacterMovement: character movement scripts
  * /ScriptableObjects: SO definitions and PersistentSingleton base class
  * /UI: UI components and systems
  * /Gear: Scripts relating to gear (items)



##### Systems Overview



The item system operates on two layers: data layer and UI layer.

###### 

###### Shop:

On the data layer: it has an inventory of items (assigned in the inspector)

On the UI layer: it creates rows with item data and "BUY" buttons



###### Wallet:

Data layer: holds the player's money and can be queried if you have enough to buy something

UI layer: Number display formatted like a currency



###### Buying Manager:

Data layer: manages buying items from the shop, and moving them between inventories



###### Player Inventory:

Data layer: has an inventory of items

UI layer: creates rows with item data and "EQUIP" buttons



###### Inventories Manager:

Allows access to both inventories by query



###### Player Gear Manager:

Data: 

* tracks equipped items in slots. 
* can equip and unequip items. 
* Has slots that can hold one item of a specific type. 
* when equipping an item it will go to the first available slot of it's type, and be removed from the inventory
* you can unequip an item from a slot, it will go to the inventory

UI: Slots that show item data and have unequip buttons



###### Player Stats Manager:

Data: holds the current stats. calculates the new stats whenever an item is equipped/unequipped

UI: displays stat values



###### Character Movement:

Character has scripts for movement, and they inherit an interface for being affected by stat changes. They are subscribed to the unity event that gets invoked when stats change.



##### Limitations



* Adding more items and viewing all of them isn't the easiest. There isn't a centralized data table for all of them.
* There isn't a save system



### Theory Questions



Q1 - What is a ScriptableObject and why is it useful? When would you use one instead of a MonoBehaviour?



A1: It’s a kind of custom unity asset that has both data and functionality. They are mostly used to store static game data and load it easily. Data such as the shop items in this task. They can also be used as editor tools to create or modify other assets. 

You would use a scriptable object in place of a MonoBehaviour when you need access to data that is not coupled to a specific scene. We may need to access the shop from several scenes, which is why we need the data to be stored outside of them, and ScriptableObject is a good solution for that.

Theoretically, you could also have a MonoBehavior on a prefab which obviously exists outside the scenes, but editing the data on this MonoBehaviour will be much more cumbersome. Also, you’d have to instantiate it in the scene, which is a redundant GameObject. 



Q2 - Explain the Single Responsibility Principle in your own words. How would you extend your system to support new stat types without rewriting existing code?



A2: The Single Responsibility Principle states that each class and method in the code should only have one responsibility. This is especially apparent in Unity’s composition architecture, where if you follow this principle you can have a very modular and easy to maintain project.



Consider as an example the following scenario: You have a 2d top down game with a controllable character that aims towards the mouse and moves with WASD. You should implement these two behaviours in two different components, this will allow you to easily add a new character that only aims but doesn’t move (like a turret), without changing any code.

Another advantage is in the clarity of this code: having two scripts named “Movement” and “AimToMouse” is easier to understand then one scripts named “CharacterMovement” or “MoveAndLookAtMouse”



Extending the system to new stat types is as easy as adding a new entry in the StatTypes enumeration.


































































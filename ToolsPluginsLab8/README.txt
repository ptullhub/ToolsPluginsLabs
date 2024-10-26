Object Pooling - This was achieved via an array that held the bullets placed in the scene, and any time the Shoot() function is enacted, 
a for loop searches for the first avaliable bullet to use before a shooting cooldown coroutine was started

Builder - There is a Target class that has the constructer elements for different targets and a ITargetBuilder interface that defines methods to apply the target properties.
ConcreteTargetBuilder implements the interface and TargetRegistry defines the different kinds of targets that can be declared and is open to extension

Observer - MovingTarget declares an OnTargetDestroyed event that the IncrementScore method in GameManager subscribes to, so any time a target is destroyed the scoreValue is
automatically passed to the GameManager and the method is invoked

LAB 9 README -
All of the save related code can be found in the SaveSystem.c file, it houses all of the definitions and methods for saving. The save system for the scoring is implemented 
via an ISaveable interface that serializes the players score and then stores it to a filepath located in C:/Users/YourUsername/Documents. For the transform saves, I created
a JSONSaveSystem class that has its own saving logic for JSON. This does not implement the ISaveable interface because my methods needed for saving the JSON file were 
incompatible with the method signatures in ISaveable so it did not make sense to implement it here. In the GameManager script, I added a SaveGame() and LoadGame() method that 
are called on Awake() and OnApplicationQuit() appropriately that passes in the proper values to my save system objects. This way if I wanted to add more stuff to save I could
simply extend these methods instead of muddying up the Awake() and OnApplicationQuit() methods.
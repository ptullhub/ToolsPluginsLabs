Object Pooling - This was achieved via an array that held the bullets placed in the scene, and any time the Shoot() function is enacted, 
a for loop searches for the first avaliable bullet to use before a shooting cooldown coroutine was started

Builder - There is a Target class that has the constructer elements for different targets and a ITargetBuilder interface that defines methods to apply the target properties.
ConcreteTargetBuilder implements the interface and TargetRegistry defines the different kinds of targets that can be declared and is open to extension

Observer - MovingTarget declares an OnTargetDestroyed event that the IncrementScore method in GameManager subscribes to, so any time a target is destroyed the scoreValue is
automatically passed to the GameManager and the method is invoked


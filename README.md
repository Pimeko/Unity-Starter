



# Unity-Starter
> Basic folders and utilities for Unity projects

- [Events](#events)
- [Variables](#variables)
- [Tools](#tools)
	- [Animation](#animation)
	- [Coroutine](#coroutine)
	- [Debug](#debug)
	- [Float](#float)
	- [Distribution](#distribution)
	- [Encoder](#encoder)
	- [Generator](#generator)
	- [Input](#input)
	- [Interaction](#interaction)
	- [Native callbacks](#native-callbacks)
	- [Save Load system](#save-load-system)
	- [Pool](#pool)
	- [Scaler Data](#scaler-data)
	- [Scene](#scene)
	- [State Machine](#state-machine)
	- [Transform](#transform)
	- [UI](#ui)
	- [Vibration](#vibration)

# Events

According to the [amazing talk of Ryan Hipple on Scriptable Objects](https://youtu.be/raQ3iHhE_Kk).

Let's say when the player jumps, you want a sound to play and the UI to display "Nice jump!". The most common way is to have a singleton that deals with sound effects, and one with the UI. From the Player Controller script, you may then do :
```c#
void Jump()
{
	// Make the player jump
	SoundSingleton.Instance.PlaySound();
	UISingleton.Instance.PlaySound();
}
```
This way of doing implies a deep link between the object that did an action and the ones that react to it. Based on the **Single Responsibility Pattern**, all the components should only do one single thing, and should have only one reason to fail. 

In a perfect world, when the player jumps, he would shout to everyone "I JUMPED!" and everyone that feels concerned should react appropriately. This way, the player, the sound manager and the UI manager don't have any link between them, and are connected through this event only.

**Game Events** are a way to keep the behaviours separated and disconnect all components that reference others, without having to use a Singleton. Each event is a scriptable object that notifies every concerned object when the action is done.

#### Basic game events
Creating a game event is dead simple. Simply right click in the project view > Event > Basic.
Then, on each GameObject that want to register to this event, add a BasicGameEventListener component and drag the scriptable object in the "event" field. Finally,  drag the action you wish to do when the event is raised. 

*Example:*
```C#
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	BasicGameEvent playerJump;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}

	void Jump()
	{
		transform.position += new Vector3(0, 10, 0);
	
		// Raises the event to all the listeners
		playerJump.Raise();
	}
```

Then in the inspector:
![](https://i.imgur.com/x9O4PA6.png)

![](https://i.imgur.com/Gmbqhwv.png)

When the player jumps, the player controller script raises the event. Each concerned object then reacts accordingly through the inspector. The Sound Manager plays its sound, the concerned text updates its value, and all the behaviours are completely independant.

##### Notes
- You have the possibility to add multiple game events to subscribe to at once
- You can delay the execution of the actions thanks to the "delay before action" field
- Ordered allows to have a real order of execution for the actions. Indeed, the UnityEvent serialized order is not reliable, and it may be useful in some cases


#### Payloaded game events
Working exactly as the basic game events, you have the possibility to create payloaded game events, i.e. adding an object to an event.

Let's say the player can change his nickname. When he validates his new name, you may want to raise an event with the new pseudo. Each payload type must have its own Event and Listener classes. But no worries: a custom inspector has been made to make your life easy!

Open Custom > Event > Payloaded > Generate and write the payload's type, as follow:
![](https://i.imgur.com/HL52xas.png)
This will generate two scripts : GameEventString and GameEventStringListener. You can then create the game event "ChangedPseudo" and add the listener to the concerned objects.

*Example:*
```C#
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	GameEventString changedPseudo;

	void UpdatePseudo(string newPseudo)
	{	
		// Raises the event to all the listeners
		changedPseudo.Raise(newPseudo);
	}
```
Then, in the inspector, using the dynamic parameters, you can pass in the new pseudo to a class method that accepts a string as a parameter.
![](https://i.imgur.com/Vhddqcp.png)
When the player changes his pseudo, it will print the new string.

##### Notes
- You have the possibility to remove all payloaded game event scripts with the tool in Custom > Event > Payloaded > Delete 

# Variables

TODO

# Tools

A compilation of amazing tools of all kinds.

## Animation
> Location: General/Utils/Animation

#### Random Speed

Changes the speed parameter of an animator to a random value between the specified ranges.

#### Over Receiver

Contains an action to trigger when an animation is over. To place on the same level as an Animator Controller to easily subscribe to animation ending events.

## Coroutine
> Location: General/Utils/Coroutine

#### Do After Delay
Easy way to delay an action through this static method.
To call an action after one second, simply do :

```C#
StartCoroutine(CoroutineUtils.DoAfterDelay(() => { // Do Action }, 1f);
```

## Debug
> Location: General/Utils/Debug

#### String Printer

Prints a string. Useful to quick test stuff in the inspector. 
*Example :* add an event listener and make it print a string to check if it is correctly called. 

#### Draw Point
Draws a point in 3D space in the scene view.
*Example :*
```c#
DebugUtils.DrawPoint(Vector3.Zero);
```
![DrawPoint](https://i.imgur.com/SBvzlma.png =500x400)

## Float
> Location: General/Utils/Float

### Float radius
Allows to adjust a float value through the scene view's gizmos.
![enter image description here](https://i.imgur.com/l2W9hzW.png =500x400)
## Distribution
> Location: General/Utils/Distribution

It allows easy **percentage distribution** across different values, based on **weights**. Its generic implementation allows **any type** for the returned value.

### How to use

To create a new Distribution type, you must declare two empty classes :
- the `distribution item`: must be serializable and inherit from DistributionItem\<**T**>
	- **T** is the type of an item's value (int, string ..)
- the `distribution` class: must inherit from Distribution<**T**, **T_ITEM**>
	- **T** is the type of an item's value (int, string ..)
	- **T_ITEM** is the type of the `distribution item` 

Don't forget to use the namespace `WeightedDistribution`.

**Example:**
Let's say you want to create a distribution to get a random string.

You simply have to create a script called `StringDistribution.cs`, as follows :
```C#
using WeightedDistribution;

[System.Serializable]
public class StringDistributionItem : DistributionItem<string> {}

public class StringDistribution: Distribution<string, StringDistributionItem > {}
```

You can now add your component to any gameobject.

Changing the weight automatically affects the probability of apparition. The percentage value is only used as a visual representation and, thus, cannot be changed directly.

You can now reference the distribution component by script and call Draw() to get a random value, as detailed in the API section.

### API
#### Draw ()

Calling draw returns a random item from the list.

**Example:**
```c#
StringDistribution distribution;

void Start()
{
	distribution = GetComponent<StringDistribution>();
}

void GetRandom()
{
	string randomString = distribution.Draw();
	// Do stuff with the random string
}
```

#### Add (T value, float weight)
Adds an item to the list.

- `Value` must be of the type of an item's value (int, string ..)
- `Weight` is a positive float value

#### RemoveAt (int index)
Removes an item from the list at the specified index.

## Encoder
> Location: General/Utils/Encoder

#### Base64Encoder

Allows to **encode** and **decode** a file in base 64, without any class instance with static methods. Useful to encrypt data in an external file.

## Generator
> Location: General/Utils/Generator

#### RandomGenerator

Instantiate a random prefab once, with position and rotation offsets, along with a percentage of spawn.

## Input
> Location: General/Utils/Input

#### PointerInteractable

Triggers an event when the user keeps on clicking and when he releases. It works with an interactable UI (UI Image for example).

#### InputController

Fills a registerable variable when the player touches the screen and moves the finger. It also works in the editor with the mouse and click.
It can be useful to subscribe to input changes, react to drags etc. It must be binded to a PointerInteractable component.


## Interaction
> Location: General/Utils/Interaction

#### OnCollisionController

Serialized events in the inspector when the object detects tags-based collisions.

#### OnTriggerController

Serialized events in the inspector when the object detects tags-based triggers.

Example : This will activate an animation when the collider triggers a gameobject with a "Player" tag.
![OnTrigger](https://i.imgur.com/IREhGQf.png)

## Native callbacks
> Location: General/Utils/NativeCallbacks

A native callback is a **unity event** called on basic Monobehaviour's methods, such as **Start**, **OnEnable** and so on. It can be very useful when prototyping, to have fast access to method calls and such, or to set behaviours on object disabling etc.

#### OnStartCallback

Invoke the actions in the unityEvent when the gameobject starts.

#### OnEnableCallback

Invoke the actions in the unityEvent when the gameobject is enabled.

#### OnDisableCallback

Invoke the actions in the unityEvent when the gameobject is disabled.

## Save Load system
> Location: General/Utils/PlayerData

Custom and secure **save/load** file system. It creates a link between the data from an external file and their associated [Registerable Variables](#variables).

### Datas
Let's say you want to save the number of coins earned by the player. 
- On load, the data is taken from an external file and is put in an IntVariable, i.e. a scriptable object that only contains an int value. 
- On save, the data is taken from the IntVariable and is written to the file.

To create such a data, you must create a class that inherits from PlayerDataVariable\<T>, where T is the type of the Registerable variable. You must also add the variable that will be written to the external file to the partial class PlayerData. Then, you only need to override the Init, Save and Load methods to handle the behavior on these events.

Once this is done, you only need to add this script to any gameobject and drag the associated registerable variable.

The data is a **JSON** formatted file, which is then encoded in **base 64**.

*Example :*
The following code will allow the save/load of the player's best score.

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Override" the PlayerData class to add the property 
public partial class PlayerData
{
    public int bestScore;
}

public class PlayerDataBestScore : PlayerDataVariable<IntVariable>
{
    // Set the initial value to 0
    public override void Init(ref PlayerData playerData)
    {
        playerData.bestScore= 0;
    }

    // Gets the data from the scriptable object 
    public override void Save(ref PlayerData playerData)
    {
        playerData.bestScore= variable.Value;
    }

    // Saves the data to the scriptable object
    public override void Load(PlayerData playerData)
    {
        variable.Value = playerData.bestScore;
    }
}
```

In the inspector, simply drag the scriptable objects that will contain the data :
![](https://i.imgur.com/e4ArXrT.png)

### Save/Load triggers
Once all your datas are created, you must use an instance of a controller to detect when to Save and Load. For that, you simply need to add the PlayerDataController script to any gameobject. 
You can then add the [game events](#events) that will raise the Save or Load. You can also specify to call the Save method when specific registerable variables change.

*Example :* 

![](https://i.imgur.com/C1wfxWC.png)

This will save the data to the external file when the event finish is raised or when the variable NbCoins changes.

*Notes :*
- The "JSON_ONLY" parameter must be used for debug purpose only. This will ensure the data is not encrypted, in order to be **human-readable**.
- The data is saved in the **folder** "PersistentDataPath/data" in an extension-less file.

### Editor tools
Under the Custom/PlayerData tab, there are tools to have an easier data management. 

#### Reset
Simply removes the file containing all the datas.

#### Generate variable

Allows you to generate a player data variable easily, without any code.

![](https://i.imgur.com/LKcOdBQ.png)

All the following fields are mandatory:
 - Name : the name of the variable in the file
 - Serialized type : the type of the data. It can be a custom class, but it must be serializable (think of using the *[System.Serializable]* attribute on the class)
 - Scriptable Object Type : the type of the registerable variable that will contain the data
 - Default value : the litteral string will be copied in the generated class, so ensure it is correctly typed
 
 This class is then generated in  `Assets/Scripts/General/Utils/PlayerData/Variables/PlayerData`. You can then move the safe anywhere safely.

## Pool
> Location: General/Utils/Pool

TODO

## Scaler Data
> Location: General/Utils/ScalerData

TODO

## Scene
> Location: General/Utils/Scene

TODO

## State Machine
> Location: General/Utils/StateMachine

TODO

## Transform
> Location: General/Utils/Transform

TODO

## UI
> Location: General/Utils/UI

TODO

## Vibration
> Location: General/Utils/Vibration

TODO






# Unity-Starter
> Basic folders and utilities for Unity projects

- [Events](#events)
- [Variables](#variables)
- [Tools](#tools)
	- [Animation](#animation)
	- [Attributes](#attributes)
	- [Camera](#camera)
	- [Debug](#debug)
	- [DelayedUnityEvent](#delayedunityevent)
	- [Dictionary](#dictionary)
	- [Distribution](#distribution)
	- [Encoder](#encoder)
	- [Float](#float)
	- [Framerate](#framerate)
	- [Generator](#generator)
	- [Input](#input)
	- [Interaction](#interaction)
	- [List](#list)
	- [Material](#material)
	- [Movement](#movement)
	- [Native callbacks](#native-callbacks)
	- [Offset](#offset)
	- [Particles](#particles)
	- [Save Load system](#save-load-system)
	- [Pool](#pool)
	- [Ragdoll](#ragdoll)
	- [Scaler Data](#scaler-data)
	- [Scene](#scene)
	- [State Machine](#state-machine)
	- [State Type](#state-type)
	- [Text](#text)
	- [Timer](#timer)
	- [Transform](#transform)
	- [UI](#ui)
	- [Vibration](#vibration)


WARNING: This plugin requires Odin and DOTween to work!

# Events

According to the [amazing talk of Ryan Hipple on Scriptable Objects](https://youtu.be/raQ3iHhE_Kk).

Let's say when the player jumps, you want a sound to play and the UI to display "Nice jump!". The most common way is to have a singleton that deals with sound effects, and one with the UI. From the Player Controller script, you may then do :
```c#
void Jump()
{
	// Make the player jump
	SoundSingleton.Instance.PlaySound();
	UISingleton.Instance.DisplayNiceJump();
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
- The "Ordered" property allows to have a real order of execution for the actions. Indeed, the UnityEvent serialized order is not reliable, and it may be useful in some cases


#### Game events with parameters
Working exactly as the basic game events, you have the possibility to create payloaded game events, i.e. sending an object of any type, along with the event.

Let's say the player can change his nickname. When he validates his new name, you may want to raise an event with the new pseudo. Each listener might then react differently based on the pseudo chosen, display it and so on.

Each payload type must have its own Event and Listener classes. But no worries: a custom inspector has been made to make your life easy!

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

Let's say the player has an HP value stored in a variable that you want to display on the User Interface.  This value often changes as the player often takes damages.

One way to do so is to have a Singleton that references the UI. Then in the player script, you might do something like :

```c#
int hp;

void Start()
{
	hp = 100;
}

void OnTakeDamage()
{
	hp -= 10;
	
	UISingleton.Instance.UpdateHpText(hp);
}
```

This implies a deep connection between the player and the UI, along with the necessity of having a singleton (or at least static references).

One elegant way to solve this problem is using registerable variables. Each variable is a scriptable object. Anyone can subscribe to the variable changes and react accordingly, with no correlation whatsoever other than the scriptable object.

In the previous example, simply create an IntVariable named "PlayerHP" in the project hierarchy. In the player script, reference this variable and modify it like so :

```c#
[SerializeField]
IntVariable playerHP;

void Start()
{
	playerHP.Value = 100;
}

void OnTakeDamage()
{
	playerHP.Value -= 10;
}
```

Then on the script that will update the text on screen, you just need to subscribe to the variable changes.

 ```c#
[SerializeField]
IntVariable playerHP;

Text uiText;

void Start()
{
	playerHP.AddOnChangeCallback(OnTakeDamage);
}

void OnTakeDamage()
{
	uiText.text = playerHp.Value.ToString();
}

void OnDestroy()
{
	playerHP.RemoveOnChangeCallback(OnTakeDamage);
}
```

The data is completely agnostic of any logic, there is no deep link between controllers : only modifiers and listeners are used. Like so, each data that must be shared with other unlinked scripts can be stored in a registerable variable. Scriptable objects being assets, you can drag them in the inspector directly before any build, and ensure your data is initialized and ready to be used at runtime.

Each type of variable must be created. To create one, simply use the menu tool in Custom > Variable > Generate. You have the option to specify if the variable is a list. This option allows to have *Add* and *Remove* methods that trigger changes.

##### Notes
- By default, the data will not trigger any change if the new value is the same as the one before. To force this behaviour, you must enable the "Check if same value" property on the scriptable object
- Don't forget to unsubscribe to changes on the gameobject destroy callback, to avoid any error
- It may be interesting to use Game Events with parameters instead of registerable variables in some cases. Simply study your best option, but both options are usually valid.


#### On Variable Change
> Location: General/ScriptableObjects/Variables/_Definitions/Utils

A basic inspector script to call serialized actions when a variable changes.
# Tools

A compilation of amazing tools of all kinds.

## Animation
> Location: General/Utils/Animation

#### Random Speed

Changes the speed parameter of an animator to a random value between the specified ranges.

#### Over Receiver

Contains an action to trigger when an animation is over. To place on the same level as an Animator Controller to easily subscribe to animation ending events.

## Attributes
> Location: General/Utils/Attributes

#### Tags

Use the attribute [Tag] to display all the availables tags in the inspector.

#### Layers

Use the attribute [Layer] to display all the availables layers in the inspector.

## Camera
> Location: General/Utils/Camera

#### Shakes

A set of useful camera shakes. Place this script on a camera component and simply the methods you need to shake it! The methods take a nullable callback when the animation is over.

Available shakes:
- Small shake vertical
- Shake vertical
- Long shake vertical
- Small shake horizontal
- Shake horizontal
- Long shake horizontal
- Small zoom out zoom in
- Zoom out zoom in elastic

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
![DrawPoint](https://i.imgur.com/SBvzlma.png)

## DelayedUnityEvent
> Location: General/Utils/DelayedUnityEvent

DelayedUnityEvent is an action serialized in the inspector with the option of adding a delay before it gets called. Instead of using the built-in UnityEvent class, it uses the amazing plugin BetterEvent, which allows to call methods with more than one parameter in the inspector, along with calling non-object related methods, such as Instantiate, Destroy, etc.

To use it, simply declare a field DelayedUnityEvent instead of a UnityEvent in your Monobehaviours.

## Dictionary
> Location: General/Utils/Dictionary

### Dictionary Extensions
#### AddOrUpdate(key, value)
Adds the key to the dictionary, or updates it if it's already there. Call yourDictionary.AddOrUpdate(...).

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

## Float
> Location: General/Utils/Float

### Float radius
Allows to adjust a float value through the scene view's gizmos.
![](https://i.imgur.com/l2W9hzW.png)

## Framerate
> Location: General/Utils/Framerate

### Framerate Controller
Put this script on any gameobject in your scene to make sure the targeted framerate will be the one specified in the inspector. By default, it is set to 120.

## Generator
> Location: General/Utils/Generator

#### RandomGenerator

Instantiate a random prefab once as a child of the script, with position, rotation and scale offsets, along with a percentage of spawn.

The scale vector represents an offset relative to 1. For example, if you want the scale.X value to be between .8 and 1.2, simply put .2 for the x value. 

## Input
> Location: General/Utils/Input

#### PointerInteractable

Triggers an event when the user keeps on clicking and when he releases. It works with an interactable UI (UI Image for example).

#### InputController

Fills a registerable variable when the player touches the screen and moves the finger. It also works in the editor with the mouse and click.

It can be useful to subscribe to input changes, react to drags etc. It must be binded to a PointerInteractable component on a UI Image on your canvas. On the latter, bind the OnPointerDown and OnPointerUp to the InputController.Touch and InputController.StopTouch in the inspector.

If you want to register to input changes, here is an example:

```c#
[SerializeField]
InputVariable playerInput;

void Start()
{
	playerInput.AddOnChangeCallback(OnInputChange);
}

void OnInputChange()
{
	if (playerInput.IsTouching)
	{
		if (playerInput.TouchPosition == Vector2.zero)
			// Do stuff
	}
}

void OnDestroy()
{
	playerInput.RemoveOnChangeCallback(OnInputChange);
}
```

#### Input Interactions Controller and Input Interaction Controller

Allows to detect input interactions with gameobjects in the scene. 

[Documentation to do]

#### InputTouchController
Basic script to register when the player clicks on the screen. Add this component to a UI Image on your canvas.

#### KeyInputController
Add inspector callbacks to key inputs, such as pressing space, escape, etc.

## Interaction
> Location: General/Utils/Interaction

#### InteractionDetector
Using this component, you can register callbacks to collision and trigger events directly in the inspector. The key used is the tag you want to compare to. If you want any tag, simply write the underscore character: *_*

You can precise what kind of interaction you want, whether it is OnEnter, OnStay or OnLeave. The callbacks can take a dynamic parameter, which is the Collision or a Collider (depending on the collision/trigger event type).

## List
> Location: General/Utils/List

#### ListExtensions
##### GetRandomItem()
Returns a random item in the list.

##### AllTheSameValue()
Checks if all the elements of the list have the same value.

Example:
```c#
List<int> list = new List<int>() { 1, 1, 1 };
Debug.Log(list.AllTheSameValue(x => x == 2)); // false;
Debug.Log(list.AllTheSameValue(x => x == 1)); // true;
```

##### Add(list)
Adds all the elements of a list to the list.

##### ForEach()
Applies a foreach to the list.

## Material
> Location: General/Utils/Material

#### MaterialRandomProperties
Applies the material properties directly from the inspector. 

Example:

You want to apply a random color between 3 specific colors to the second material of your mesh. Add this component, write 1 for the materialIndex. Then, in the color property, add the field's name (for example "_Color") and pick your range colors.

The same goes for int and float properties. If you want to have a completely random color without a range, you can toggle the "full random" checkbox.

#### RandomMaterial
Applies a random material from a list to the MeshRenderer.

## Movement
> Location: General/Utils/Movement

#### Path Follower


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



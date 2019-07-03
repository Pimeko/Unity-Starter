


# Unity-Starter
> Basic folders and utilities for Unity projects

- [Events](#events)
- [Variables](#variables)
- [Tools](#tools)
	- [Animation](#animation)
	- [Debug](#debug)
	- [Weighted Distribution](#weighted-distribution)
	- [Encoder](#encoder)
	- [Generator](#generator)
	- [Input](#input)
	- [Interaction](#interaction)
	- [Native callbacks](#native-callbacks)

# Events

TODO

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

## Debug
> Location: General/Utils/Debug

#### String Printer

Prints a string. Useful to quick test stuff in the inspector. 
*Example :* add an event listener and make it print a string to check if it is correctly called. 

## Weighted Distribution
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

## Player data
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

*Note :*
- The "JSON_ONLY" parameter must be used for debug purpose only. This will ensure the data is not encrypted, in order to be **human-readable**.
- The data is saved in the **folder** "PersistentDataPath/data" in an extension-less file.

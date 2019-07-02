
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

It allows easy percentage distribution across different values, based on weights. Its generic implementation allows **any type** for the returned value.

### How to use
To begin, simply download the package and add it to your Unity Project.

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

Allows to encode and decode a file in base 64, without any class instance with static methods. Useful to encrypt data in an external file.

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

A native callback is a unity event called on basic Monobehaviour's methods, such as Start, OnEnable and so on. It can be very useful when prototyping, to have fast access to method calls and such, or to set behaviours on object disabling etc.

#### OnStartCallback

Invoke the actions in the unityEvent when the gameobject starts.

#### OnEnableCallback

Invoke the actions in the unityEvent when the gameobject is enabled.

#### OnDisableCallback

Invoke the actions in the unityEvent when the gameobject is disabled.

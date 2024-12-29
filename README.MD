# WHAT IS THIS?

A "Mega Man"-like game clone that I created in my spare time with my sons, based on the Unity game engine.
Its purpose was to allow my sons to create their own Mega Man levels.  However, my kids have since lost
interest in Mega Man, and the project was set aside, but they did get a chance to and play their own levels.

NOTE: The sample includes only a portion of the entire project.  Also, the code is not well commented, as it
was written entirely for personal use.


# PROSPECTIVE EMPLOYER: KEY TAKE-AWAYS

What I want you to take away from this project is:

1. This was my first project in C#, and demonstrates that I can learn new code languages fairly quickly.

	* In this project I strove to become familiar with C# paradigms, and really enjoyed the language's simple
		interfaces, especially the setters and getters, and the 'interface' type itself.  I also appreciated
		its tight integration with database queries, especially the way predicates simplified expression.
	* It is still rough around the edges.


2. I would like to draw your attention to my work to improve the Unity API (locally within the project):

	* I created a character movement interface class to simplify Unity's relatively complex interaction with
		physical movement of spatial objects.
		
		See: Code/GG/MovementController/*
	
	* I created a general data model that tracks variables and their dependencies, allowing them to calculate
		updates only when needed.
		
		See: Code/GG/Core/*


Original README.MD

# MegaMan Prime

This project is being developed by a Dad and his sons to learn how to make your own games.

Most assets were found at:

https://github.com/MegaChibisX/MegaMan-Unity-8Bit-Engine

which is another great project.

## Requirements

* Unity version 2020.3.9f1

## Packages

* 2D Tilemap extras, installed from https://github.com/Unity-Technologies/2d-extras.git#master

## Assets

There are many unused assets in the project, but the important ones are:

* Easy Sprite Sheet Copier (throws obsolete warnings)

Test commit3

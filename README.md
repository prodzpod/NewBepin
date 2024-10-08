# New Bepinpack for Core Keeper
what the heck is a ECS

Bepin pack for core keeper 1.0. contains harmonyX and all the usual things and works with mod.io mods.

## [BepInEx](https://github.com/BepInEx/BepInEx) package
bepinex 5.4 compiled from source to make it work with netstandard 2.1. does not contain monomod or harmony dlls as it is already shipped with the game.

## BepInEx.GUI
[the one from risk of rain](https://thunderstore.io/package/RiskofThunder/BepInEx_GUI/), also compiled from source to made work with this one.

## FixModIOMods
harmonypatch on `RoslynCSharp.Compiler.AssemblyReferenceFromAssemblyObject..ctor` to acknowledge the bepin hooked `UnityEngine.CoreModule`.

## RemoveOldCecil
deletes `Managed/Mono.Cecil.dll` so the one shipped with this mod is used instead. necessary for bepin to load correctly.

## RemoveOldCecilConsole.exe
attempts to delete the dlls given by argument.
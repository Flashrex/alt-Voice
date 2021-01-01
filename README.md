# alt:Voice

## Description

alt:Voice is a custom voice API written in C# that utilizes the built in VoiceChat from the alt:V C# API.
Players get automatically added to global voice and can communicate with other players in range.

>> This Resource is currently work in progress

## Features

* 3 global voice channels (closeRange, midRange, longRange)
* Players can switch between 4 range-modes (mute, close, mid, long) using '+'-Key (Js Keycode: 107)
* Easily add your own voice channels using events
* Configure Resource without touching the code using external settings.json
* Build in debug mode to find that nasty Errors

## Events

You can create custom voice channels for example to add phone calls to your resource using the alt:V eventsystem.
alt:Voice automatically detects empty channels and deletes them for you.

```csharp
[ServerEvent("altvoice:createchannel")]
public void OnCreateChannel(int voiceid, IPlayer[] players = null) { }

[ServerEvent("altvoice:addplayer")]
public void OnAddPlayer(int voiceid, IPlayer player) { }

[ServerEvent("altvoice:removeplayer")]
public void OnRemovePlayer(int voiceid, IPlayer player) { }
```

## Usage

* C#-Serverside

* 'id' is an unique id to identify each voice channel
* alt:Voice automatically makes sure that no id gets used twice by not creating your channel if the given id is already in use

```csharp
//Create a new voice channel
alt.Emit('altvoice:createchannel', id); 

//Add a player to a voice channel
alt.Emit('altvoice:addplayer', id, player);

//Remove a player from a voice channel
alt.Emit('altvoice:removeplayer', id, player);
```

## Installation

1. Create a new resource folder in alt-server/resources
2. Drop everything from resource in your create folder
3. Add your resource in your server.cfg
4. Optional: Configure alt:Voice using the settings.json-File inside the resource folder
5. Start your server

Disclaimer: You may have to recompile the resource using the latest nuget.
Simply open altvoice/altvoice.sln and make sure to update the alt:v api nuget.
After that you can simply recompile the project.

## Other

You like my stuff?
Make sure to give me a star and follow me on social media:
* Twitter: https://twitter.com/Flashrex_
* Youtube: https://www.youtube.com/c/Flashrex
* Twitch: https://twitch.tv/flashrex

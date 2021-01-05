# alt:Roleplay Voice

![alt text](https://github.com/Flashrex/alt-Voice/blob/experimental/thumbnail.png)

## Description

alt:Voice is a custom voice API written in C# that utilizes the built in VoiceChat from the alt:V C# API.
Players get automatically added to global voice and can communicate with other players in range.

>> This Resource is currently work in progress

## Features

* 3 global voice channels (closeRange, midRange, longRange)
* Players can switch between 4 range-modes (mute, close, mid, long) using '+'-Key (Js Keycode: 107)
* Easily add your own voice channels using events
* Ingame radio/walkie-talkie usable with '.'-Key (Js Keycode:190)
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

Alt.Emit("altvoice:removedchannel", Id);
```

## Usage

'id' is an unique id to identify each voice channel.
Alt:Voice automatically makes sure that no id gets used twice by not creating your channel if the given id is already in use.

* C#-Serverside
```csharp
//Create a new voice channel
alt.Emit('altvoice:createchannel', id); 
Optional: alt.Emit('altvoice:createchannel', id, players); 

//Add a player to a voice channel
alt.Emit('altvoice:addplayer', id, player);

//Remove a player from a voice channel
alt.Emit('altvoice:removeplayer', id, player);

//Gets called if a temporary voice channel gets removed
[ServerEvent("altvoice:removedchannel")]
public void OnRemoveChannel(int channelid) {
  //Do Stuff
}

//Give/remove player radio (walkie-talkie)
//args is a bool - true = give / false = remove
player.SetStreamSyncedMetaData("altvoice:hasRadio", args);

```

* JS-Serverside
```javascript
//Create a new voice channel
alt.emit('altvoice:createchannel', id); 
Optional: alt.emit('altvoice:createchannel', id, players); 

//Add a player to a voice channel
alt.emit('altvoice:addplayer', id, player);

//Remove a player from a voice channel
alt.emit('altvoice:removeplayer', id, player);

//Gets called if a temporary voice channel gets removed
alt.on('altvoice:removedchannel', (channelid) => {
  //do stuff
})
```

## Settings

```
{
	"Debug": true, //Enable/Disable Debug
	"UseGlobalVoice": true, //Enable/Disable Global ranged Voice Channels
	"UseRadio": true, //Enable/Disable radio/walkie-talkie
	"ShortRange": 5.0, //Sets range for short range channel
	"MidRange": 10.0, //Sets range for mid range channel
	"LongRange": 20.0 //Sets range for long range channel
}
```

## Debug

You can enable debug by settings UseDebug to true in settings.json.
It enables serverside console commands + debug messages

Server Console Commands:
```
channels - Shows current active voice channels
meta <playername> - Gives a player a walkie talkie
removemeta <playername> - Take walkie talkie from given player
```

## Installation

1. Create a new resource folder in alt-server/resources
2. Drop everything from /resource in your new created folder
3. Add the resource in your server.cfg
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

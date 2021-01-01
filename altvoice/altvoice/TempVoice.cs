using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;

namespace altvoice {
    class TempVoice : IScript {

        public static List<TempVoice> VoiceChannels { get; set; } = new List<TempVoice>();

        public int Id { get; set; }
        public IVoiceChannel IVoiceChannel { get; set; }
        public List<IPlayer> Players { get; set; }

        public TempVoice() { }

        public TempVoice(int id) {
            Id = id;
            IVoiceChannel = Alt.CreateVoiceChannel(false, 0.0f);
            Players = new List<IPlayer>();
            VoiceChannels.Add(this);
            if (Settings.GetSettings().Debug) Debug.LogColored($"Created Voice Channel: {id}");
        }

        /// <summary>
        /// Adds a Player to this Voice Channel
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(IPlayer player) {
            try {
                IVoiceChannel.AddPlayer(player);
                Players.Add(player);
                if (Settings.GetSettings().Debug) Debug.LogColored($"Added player to Voice Channel: {Id} - Players: {Players.Count}");
            } catch (Exception e) {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
            }
            
            
        }

        /// <summary>
        /// Removes a Player from this Voice Channel
        /// Delete Channel if empty
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(IPlayer player) {
            if (Players.Contains(player)) {
                try {
                    Players.Remove(player);
                    IVoiceChannel.RemovePlayer(player);
                    if (Settings.GetSettings().Debug) Debug.LogColored($"Removed player from Voice Channel: {Id} - Players: {Players.Count}");
                    if (Players.Count <= 1) {
                        Delete();
                    }
                } catch (Exception e) {
                    Debug.LogError(e.Message);
                    Debug.LogError(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// Deletes this Voice Channel
        /// </summary>
        public void Delete() {
            try {
                IVoiceChannel.Remove();
                VoiceChannels.Remove(this);
                if (Settings.GetSettings().Debug) Debug.LogColored($"Removed Voice Channel: {Id}");
                Alt.Emit("altvoice:removedchannel", Id);
            } catch(Exception e) {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
            }
            
        }

        /// <summary>
        /// Mutes a player in this channel
        /// </summary>
        /// <param name="player">Player to mute</param>
        public void MutePlayer(IPlayer player) {
            IVoiceChannel.MutePlayer(player);
        }

        /// <summary>
        /// Checks if a channel with the given Id exists.
        /// </summary>
        /// <param name="id">Id to check</param>
        /// <returns></returns>
        public static bool DoesChannelWithIDExists(int id) {
            foreach (TempVoice channel in VoiceChannels) if (channel.Id == id) return true;
            return false;
        }

        /// <summary>
        /// Returns the voice Channel with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TempVoice GetVoiceChannelById(int id) {
            foreach(TempVoice channel in VoiceChannels) if(channel.Id == id) return channel;
            return null;
        }

        [ServerEvent("altvoice:createchannel")]
        public void OnCreateChannel(int voiceid, IPlayer[] players = null) {
            if (DoesChannelWithIDExists(voiceid)) {
                Debug.LogError("Create VC Error: Id already in use");
                return;
            }

            TempVoice channel = new TempVoice(voiceid);
            if(players != null) {
                for (int i = 0; i < players.Length; i++) channel.AddPlayer(players[i]);
            }
        }

        [ServerEvent("altvoice:addplayer")]
        public void OnAddPlayer(int voiceid, IPlayer player) {
            TempVoice voice = GetVoiceChannelById(voiceid);
            if (voice != null) voice.AddPlayer(player);
        }

        [ServerEvent("altvoice:removeplayer")]
        public void OnRemovePlayer(int voiceid, IPlayer player) {
            TempVoice voice = GetVoiceChannelById(voiceid);
            if (voice != null) {
                voice.RemovePlayer(player);
            }
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayersDisconnect(IPlayer player, string reason) {
            foreach(TempVoice voice in VoiceChannels) {
                if (voice.Players.Contains(player)) voice.RemovePlayer(player);
            }
        }
    }
}

using AltV.Net;
using AltV.Net.Elements.Entities;

namespace altvoice {
    class GlobalVoice : IScript {

        public static GlobalVoice globalVoice;

        public IVoiceChannel[] GlobalVoiceChannels = new IVoiceChannel[3];

        public GlobalVoice() { }

        public GlobalVoice(Settings settings) {
            GlobalVoiceChannels[0] = Alt.CreateVoiceChannel(true, settings.ShortRange);
            GlobalVoiceChannels[1] = Alt.CreateVoiceChannel(true, settings.MidRange);
            GlobalVoiceChannels[2] = Alt.CreateVoiceChannel(true, settings.LongRange);
            globalVoice = this;
        }

        /// <summary>
        /// Adds a player to all 3D Voice Channels
        /// </summary>
        /// <param name="player">Player to Add</param>
        public static void AddPlayerToAllVoiceChannels(IPlayer player) {
            for (int i = 0; i < globalVoice.GlobalVoiceChannels.Length; i++) globalVoice.GlobalVoiceChannels[i].AddPlayer(player);
        }

        /// <summary>
        /// Removes a player from all 3D Voice Channels
        /// </summary>
        /// <param name="player">Player to Remove</param>
        public static void RemovePlayerFromAllVoiceChannels(IPlayer player) {
            for (int i = 0; i < globalVoice.GlobalVoiceChannels.Length; i++) globalVoice.GlobalVoiceChannels[i].RemovePlayer(player);
        }

        /// <summary>
        /// Mutes a player in all 3D Voice Channels
        /// </summary>
        /// <param name="player">Player to Mute</param>
        public static void MutePlayerInAllVoiceChannels(IPlayer player) {
            for (int i = 0; i < globalVoice.GlobalVoiceChannels.Length; i++) globalVoice.GlobalVoiceChannels[i].MutePlayer(player);
        }

        /// <summary>
        /// Switches a players current Voice Channel/Range.
        /// </summary>
        /// <param name="player">Player to Switch</param>
        /// <param name="voiceRange">New Voice Range</param>
        public static void SwitchVoiceChannelForPlayer(IPlayer player, int voiceRange) {
            MutePlayerInAllVoiceChannels(player);
            globalVoice.GlobalVoiceChannels[voiceRange].UnmutePlayer(player);
        }

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(IPlayer player, string reason) {
            AddPlayerToAllVoiceChannels(player);
            //Standart Range
            SwitchVoiceChannelForPlayer(player, 1);

        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(IPlayer player, string reason) {
            RemovePlayerFromAllVoiceChannels(player);
        }

        /// <summary>
        /// Gets called if a player changes his voice Range
        /// </summary>
        /// <param name="player">Player who changes his range</param>
        /// <param name="range">New Range</param>
        [ClientEvent("altvoice:changerange")]
        public void OnRangeChange(IPlayer player, int range) {
            if (range == 0) MutePlayerInAllVoiceChannels(player);
            else SwitchVoiceChannelForPlayer(player, range-1);
        }
    }
}

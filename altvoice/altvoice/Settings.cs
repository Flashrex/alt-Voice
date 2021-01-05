using AltV.Net;
using AltV.Net.Elements.Entities;
using System.IO;
using System.Text.Json;

namespace altvoice {
    class Settings : IScript{

        private static Settings Config = null;

        public bool Debug { get; set; }
        public bool UseGlobalVoice { get; set; }
        public bool UseRadio { get; set; }
        public float ShortRange { get; set; }
        public float MidRange { get; set; }
        public float LongRange { get; set; }

        public Settings() {
            Debug = false;
            UseGlobalVoice = true;
            ShortRange = 5.0f;
            MidRange = 10.0f;
            LongRange = 20.0f;
            Config = this;
        }

        /// <summary>
        /// Loads Settings from Json
        /// </summary>
        /// <returns></returns>
        public static Settings LoadSettings() {
            if (File.Exists(Alt.Server.Resource.Path + "/settings.json")) {
                altvoice.Debug.LogColored("settings.json found. Load settings...");
                string json = File.ReadAllText(Alt.Server.Resource.Path + "/settings.json");
                Config = JsonSerializer.Deserialize<Settings>(json);

            } else {
                altvoice.Debug.LogColored("Couldn´t find settings.json. Using default settings...");
                new Settings();
            }
            return Config;
        }

        /// <summary>
        /// Returns current Settings
        /// </summary>
        /// <returns></returns>
        public static Settings GetSettings() {
            return Config;
        }

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(IPlayer player, string reason) {
            if(Config != null) {
                player.Emit("altvoice:isRadioEnabled", Config.UseRadio);
            }
        }
    }
}

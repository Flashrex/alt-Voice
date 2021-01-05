using AltV.Net;
using AltV.Net.Elements.Entities;
using System;

namespace altvoice {
    class Debug : IScript{

        public static void LogColored(string msg) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[AltVoice] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg + "\n");
        }

        public static void LogError(string msg) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[AltVoice] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg + "\n");
        }

        [ScriptEvent(ScriptEventType.ConsoleCommand)]
        public void OnConsoleCommand(string cmd, string[] args) {
            if (!Settings.GetSettings().Debug) return;
            switch (cmd) {
                case "channels":
                    LogColored("Active Voice Channels: " + (3 + TempVoice.VoiceChannels.Count));
                    break;
                case "meta":
                    if(args.Length == 1) {
                        string playerName = args[0];

                        foreach (IPlayer player in Alt.GetAllPlayers()) {
                            if (player.Name.ToLower() == playerName.ToLower()) {
                                player.SetStreamSyncedMetaData("altvoice:hasRadio", true);
                                LogColored("Added MetaData to " +playerName);
                                return;
                            }
                        }
                    }
                    break;
                case "removemeta":
                    if(args.Length == 1) {
                        string playerName = args[0];
                        foreach (IPlayer player in Alt.GetAllPlayers()) {
                            if (player.Name.ToLower() == playerName.ToLower()) {
                                player.SetStreamSyncedMetaData("altvoice:hasRadio", false);
                                LogColored("Removed MetaData from " + playerName);
                                return;
                            }
                        }
                    }
                    break;
            }
        }
    }
}

using AltV.Net;
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
            switch (cmd) {
                case "channels":
                    LogColored("Active Voice Channels: " + (3 + TempVoice.VoiceChannels.Count));
                    break;
            }
        }
    }
}

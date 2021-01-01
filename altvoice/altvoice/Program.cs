using AltV.Net;

namespace altvoice {
    class Program : Resource {

        public override void OnStart() {
            Debug.LogColored("Initialize ...");

            Settings settings = Settings.LoadSettings();

            if (settings.UseGlobalVoice) {
                Debug.LogColored("Creating global voice channels...");
                new GlobalVoice(settings);
            } else {
                Debug.LogColored("Global voice channels deactivated.");
            }

            Debug.LogColored("AltVoice started successfully.");
        }

        public override void OnStop() {
        }

        
    }
}

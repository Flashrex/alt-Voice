using AltV.Net;

namespace altvoice {
    class Program : Resource {

        public override void OnStart() {
            Debug.LogColored("Initialisiere ...");

            Settings settings = Settings.LoadSettings();

            if (settings.UseGlobalVoice) {
                Debug.LogColored("Erstelle globale Voice Channel...");
                new GlobalVoice(settings);
            } else {
                Debug.LogColored("Globale Channel deaktiviert.");
            }

            Debug.LogColored("AltVoice erfolgreich getartet.");
        }

        public override void OnStop() {
        }

        
    }
}

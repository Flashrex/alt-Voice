/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';

let voiceRange = 1;
let webview = null;

//Anti Spam Protection
let lastInteract = 0;
function canInteract() { return lastInteract + 500 < Date.now(); }

alt.on('connectionComplete', () => {
    webview = new alt.WebView("http://resource/client/html/index.html");
})

alt.on('keydown', key => {
    if(key == 107 && canInteract()) { // <-Key
        lastInteract = Date.now();
        voiceRange = (voiceRange+1) % 4;
        alt.log("changes Range: " +voiceRange);
        alt.emitServer('altvoice:changerange', voiceRange);
        webview.emit('changeRange', voiceRange);
    }
})

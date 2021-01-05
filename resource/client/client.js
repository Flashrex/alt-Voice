/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';

let voiceRange = 1;
let soundrange = null;

let radio = null;
let radioHidden = true;
let isRadioEnabled = false;

//Anti Spam Protection
let lastInteract = 0;
function canInteract() { return lastInteract + 250 < Date.now(); }

alt.onServer('altvoice:isRadioEnabled', (isEnabled) => {
    isRadioEnabled = isEnabled;
})

alt.on('connectionComplete', () => {
    soundrange = new alt.WebView("http://resource/client/html/index.html");
})

alt.on('streamSyncedMetaChange', (entity, key, value, old_value) => {
    if(entity.scriptID === alt.Player.local.scriptID && key === 'altvoice:hasRadio') {
        if(!isRadioEnabled) return;
        if(value) {
            if(radio) return;
            radio = new alt.WebView("http://resource/client/html/walkietalkie/index.html");
            radio.on('channel:change', (channel) => {
                if(channel == 0.0) return;
                alt.emitServer('altvoice:changeChannel', channel);
            })
        } else {
            alt.emitServer("altvoice:removedMeta");
            if(!radioHidden) {
                radio.emit("toggleRadio", false);
                alt.toggleGameControls(true);
                alt.showCursor(false);
                radioHidden = true;
                
            }
            alt.setTimeout(() => {
                radio.destroy();
                radio = null;
            }, 750)
        }
    }
})

alt.on('keydown', key => {
    if(!canInteract()) return;
    lastInteract = Date.now();
    if(key == 107) { // <-Key
        voiceRange = (voiceRange+1) % 4;
        alt.log("changes Range: " +voiceRange);
        alt.emitServer('altvoice:changerange', voiceRange);
        soundrange.emit('changeRange', voiceRange);
        return;
    }
    
    if(radio === null || !isRadioEnabled) return;
    if(key == 190) { //.-Key
        if(radioHidden) {
            radio.emit('toggleRadio', true);
            alt.showCursor(true);
            alt.toggleGameControls(false);
            radio.focus();

        } else {
            radio.emit('toggleRadio', false);
            alt.showCursor(false);
            alt.toggleGameControls(true);
            radio.unfocus();
        }
        radioHidden = !radioHidden;
        return;
    } 
    
    if(radioHidden) return;
    if(key === 38) { //Up-Arrow
        radio.emit('channel:increase');

    } else if(key === 40) { //Down-Arrow
        radio.emit('channel:decrease');
    } else if(key === 13) { //Enter
        //Todo: Play sound
        radio.emit('channel:confirm');
    }

})

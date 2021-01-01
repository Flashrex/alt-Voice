function changeRange(range) {
    switch(range) {
        case 0:
            //muted
            document.getElementById('mute').hidden = false;
            document.getElementById('short').hidden = true;
            document.getElementById('mid').hidden = true;
            document.getElementById('long').hidden = true;
            break;
        case 1:
            //short
            document.getElementById('mute').hidden = true;
            document.getElementById('short').hidden = false;
            document.getElementById('mid').hidden = true;
            document.getElementById('long').hidden = true;
            break;
        case 2:
            //mid
            document.getElementById('mute').hidden = true;
            document.getElementById('short').hidden = true;
            document.getElementById('mid').hidden = false;
            document.getElementById('long').hidden = true;
            break;
        case 3:
            //long
            document.getElementById('mute').hidden = true;
            document.getElementById('short').hidden = true;
            document.getElementById('mid').hidden = true;
            document.getElementById('long').hidden = false;
            break;
    }
}

alt.on('changeRange', (range) => changeRange(range));
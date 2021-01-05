let tempChannel = 0;
let currentChannel = 0;

function toggleRadio(toggle) {
    if(toggle) slideIn();
    else slideOut();
}

function updateChannel(increase) {
    if(increase) {
        tempChannel < 999 ? tempChannel += 1.0 : tempChannel = 0;
    } else {
        tempChannel > 0 ? tempChannel -= 1.0 : tempChannel = 999;
    }

    
    if(tempChannel < 10.0) document.getElementById("channel").innerHTML = "00" +tempChannel +".00";
    else if(tempChannel < 100.0) document.getElementById("channel").innerHTML = "0" +tempChannel +".00";
    else document.getElementById("channel").innerHTML = tempChannel +".00";
}

function updateTime() {
    document.getElementById("time").innerHTML = GetCurrentTimeString();
    setInterval(() => {
        document.getElementById("time").innerHTML = GetCurrentTimeString();
    }, 1000);
}

function GetCurrentTimeString() {
    var d = new Date();
    var hours = d.getHours();
    if(hours < 10) hours = '0' + hours;
    var minutes = d.getMinutes();
    if(minutes < 10) minutes = '0' + minutes;

    return '' +hours +':' +minutes;
}

function confirmChannel() {
    if(tempChannel === 0.0 || tempChannel !== currentChannel) {
        currentChannel = tempChannel;
        alt.emit("channel:change", currentChannel);
    }
}

function slideIn() {
    let elem = document.getElementById('radio');

    elem.animate([
        {
            bottom: '-600px'
        },
        {
            bottom: '-40px'
        }
    ], {
        duration: 750,
        easing: "ease-out"
    });
    elem.style.bottom = '-40px';
}

function slideOut() {
    let elem = document.getElementById('radio');

    elem.animate([
        {
            bottom: '-40px'
        },
        {
            bottom: '-600px'
        }
    ], {
        duration: 750,
        easing: "ease-in"
    });
    elem.style.bottom = '-600px';
}

window.onload = function() {
    updateTime();
}

alt.on('toggleRadio', (toggle) => toggleRadio(toggle));
alt.on('channel:increase', () => updateChannel(true));
alt.on('channel:decrease', () => updateChannel(false));
alt.on('channel:confirm', () => confirmChannel());
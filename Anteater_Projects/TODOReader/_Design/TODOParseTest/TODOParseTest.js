const XMLHttpRequest = require('xmlhttprequest').XMLHttpRequest;

const url = 'https://raw.githubusercontent.com/anteater333/Anteater_lab/master/Txt/TODOs.txt';
const xhr = new XMLHttpRequest();

xhr.onreadystatechange = function() {
    if (xhr.readyState === xhr.DONE) {
        console.log(xhr.responseText);
    }
}

xhr.open('GET', url);
xhr.send();
const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 0; // 입력 갯수

let IGotT = false;

rl.on('line', function(line) {
    input.push(line);
    if (!IGotT) {
        inputNum = parseInt(input.pop());
        IGotT = true;
    }
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

// (면의 수|F) = 2 - (꼭짓점의 수|V) + (모서리의 수|E)
function F(V, E) {
    return 2 - parseInt(V) + parseInt(E);
}

function algorila() {
    input.forEach(val => {
        console.log(F(...val.split(' ')));
    })
}
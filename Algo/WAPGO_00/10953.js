/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 0; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == 0) {
        inputNum = line
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    input.forEach(sum => {
        console.log(parseInt(sum.split(",")[0]) + parseInt(sum.split(",")[1]))
    });
}
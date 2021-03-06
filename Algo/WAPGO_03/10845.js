/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = parseInt(line.split(' ')[0])
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {

    algorila();

    process.exit();
});

let myQ = []

function algorila() {
    for(let i = 0; i < inputNum; i++) {
        cmd = input[i]
        if (cmd.includes('push')) {
            myQ.push(cmd.split(' ')[1])
        }
        else if (cmd.includes('pop')) {
            console.log((myQ.length == 0) ? -1 : myQ.shift())
        }
        else if (cmd.includes('size')) {
            console.log(myQ.length)
        }
        else if (cmd.includes('empty')) {
            console.log((myQ.length == 0) ? 1 : 0)
        }
        else if (cmd.includes('front')) {
            console.log((myQ.length == 0) ? -1 : myQ[0])
        }
        else if (cmd.includes('back')) {
            console.log((myQ.length == 0) ? -1 : myQ[myQ.length - 1])
        }
    }
}
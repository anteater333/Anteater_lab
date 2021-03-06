/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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

function algorila() {
    let cntTrue = 0;
    let cntFalse = 0;
    let S = input.pop()

    let preBit = S.charAt(0)
    let curBit = ""

    for (let idx = 1; idx < S.length; idx++) {
        curBit = S.charAt(idx)
        if (curBit != preBit) {
            if (preBit == '1') cntTrue++
            else cntFalse++
            preBit = curBit
            continue
        }
    }

    if (curBit == '1') cntTrue++
    else cntFalse++

    console.log((cntTrue > cntFalse) ? cntFalse : cntTrue)
}
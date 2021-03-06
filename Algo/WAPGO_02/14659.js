/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == -1) {
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
    const N = parseInt(input[0])
    const hanzos = input[1].split(' ')

    let highest = 0
    let bestScore = 0

    for (let idx = 1; idx < N; idx++) {
        if (parseInt(hanzos[highest]) < parseInt(hanzos[idx])) {
            let curScore = idx - highest - 1
            bestScore = (bestScore > curScore) ? bestScore : curScore
            highest = idx
        }
    }

    lastScore = N - highest - 1
    bestScore = (bestScore > lastScore) ? bestScore : lastScore

    console.log(bestScore)
}

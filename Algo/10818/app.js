const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    const N = input.shift();
    const numbers = input.shift().split(" ", N).map((num) => {
        return parseInt(num);
    });

    let MAX = -1000000;
    let MIN = 1000000;

    for (let i = 0; i < N; i++) {
        if (numbers[i] > MAX) MAX = numbers[i];
        if (numbers[i] < MIN) MIN = numbers[i];
    }

    console.log(`${MIN} ${MAX}`);
}
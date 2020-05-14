const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    const minmax = input.pop().split(' ');
    const min = parseInt(minmax[0]);
    const max = parseInt(minmax[1]);
    const total = max - min + 1;    // 주어진 범위에서의 전체 자연수 개수
    
    const yesSquares = new Map();
    
    for (let i = 2; i ** 2 <= max; i++) {
        let square = i ** 2;
        for (let j = Math.ceil(min/square); square * j <= max; j++) {
            if (!yesSquares.has(square * j))
                yesSquares.set(square * j, true);
        }
    }

    console.log(total - yesSquares.size);
}
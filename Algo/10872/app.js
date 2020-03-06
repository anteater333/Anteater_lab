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
    const factorial = (n) => {
        if (n == 1 || n == 0) return 1;
        else return n * factorial(n-1);
    }

    console.log(factorial(input.shift()));
}
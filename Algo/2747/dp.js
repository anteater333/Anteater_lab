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
    console.log(nazzi(parseInt(input.pop())));
}

let memo = Array(100);
function nazzi(n) {
    if (n <= 1) return n;
    else if (memo[n]) return memo[n];
    else {
        memo[n] = nazzi(n - 1) + nazzi(n - 2);
        return memo[n];
    }
}
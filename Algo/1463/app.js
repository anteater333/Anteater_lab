/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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

/**
 * rule
 * 1. 3으로 나눈다 (단, 3의 배수일 때)
 * 2. 2로 나눈다 (단, 2의 배수일 때)
 * 3. 1을 뺀다
 */

const memo = [0, 0] // memo[0] : impossible, memo[1] : 0

function algorila() {
    let target = parseInt(input.shift())

    for (let i = 2; i <= target; i++) {
        memo[i] = memo[i-1] + 1 // rule 3
        if (i % 2 == 0) { // rule 2
            memo[i] = Math.min(memo[i], memo[i/2] + 1)
        }
        if (i % 3 == 0) { // rule 1
            memo[i] = Math.min(memo[i], memo[i/3] + 1)
        }
    }
    console.log(memo[target])
}

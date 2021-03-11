/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

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

/*

a[n] = a[n-1] + a[n-2] + a[n-3]
(a[n]은 a[n-1] + 1의 경우, a[n-2] + 2의 경우, a[n-3] + 3의 경우를 합한 수)

*/

const memo = [0, 1, 2, 4]

function algorila() {
    while (input.length != 0) {
        let cur = parseInt(input.shift())
        
        if (cur >= memo.length) {   // not yet memoized
            for (let i = memo.length; i <= cur; i++) {
                memo[i] = memo[i - 1] + memo[i - 2] + memo[i - 3]
            }
        }

        console.log(memo[cur])
    }
}

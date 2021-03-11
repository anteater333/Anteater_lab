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

function algorila() {

    const wine = [0, ]

    input.forEach(element => {
        wine.push(parseInt(element))
    });

    if (wine.length == 2) { // n == 1인 경우
        console.log(wine[1])
        return  // 이런 얄팍한 수에 넘어가지 않도록 문제를 잘 보자
    }

    // 이해가 잘 안된다면
    // 와인을 앞에서부터 i잔 째 마신 최선값에대한 메모라고 생각해보자.
    // memo[0] : 한 잔도 안마셨으니까 0
    // memo[1] : 첫 번째 잔. 한 잔 끝
    // memo[2] : 첫 번째 잔 + 두 번째 잔(연달아 3잔 불가 규칙 위반 X)
    const memo = [0, wine[1], wine[1] + wine[2]]

    for(let i = 3; i < wine.length; i++) {
        // 직전 와인 거르고 새로 마실 때 vs 전전에 한 번 거르고 2번 연속 마실 때
        memo[i] = Math.max(memo[i-2] + wine[i], memo[i-3] + wine[i] + wine[i-1])
        
        memo[i] = Math.max(memo[i], memo[i-1])  // 이번 와인을 거르는게 더 나을 때
    }

    console.log(memo.pop())
}

/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let goal = 0;

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = parseInt(line.split(' ')[0])
        goal = parseInt(line.split(' ')[1])
    }
    else {
        input.push(parseInt(line));
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {

    algorila();

    process.exit();
});

function algorila() {
    let count = 0
    let remain = goal
    
    while (remain > 0) {
        let coin = input.pop()
        let coinCount = parseInt(remain / coin)
        remain -= coin * coinCount
        count += coinCount
    }

    console.log(count)
}
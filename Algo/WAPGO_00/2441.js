
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
    const count = parseInt(input[0])
    const space = " "
    const star = "*"
    for (let i = 0; i < count; i ++) {
        console.log(space.repeat(i) + star.repeat(count - i))
    }
}
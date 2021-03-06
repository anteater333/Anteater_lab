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
    const N = parseInt(input[0])
    const space = " "
    const star = "*"
    for (let i = 1; i <= N; i++) {
        if (i == N) {// Last line
            console.log(star.repeat(N * 2 - 1))
            break;
        }
        let line = space.repeat(N - i) + star
        if (i == 1) {
            console.log(line)
            continue;
        }
        line += space.repeat(2 * i - 3) + star
        console.log(line)
    }
}
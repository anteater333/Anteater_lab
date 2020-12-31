/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = 0    // "고무오리 디버깅 시작"
    }
    else {
        if (line == '고무오리 디버깅 끝')
            rl.close()

        input.push(line);
    }
}).on('close', function() {

    algorila();

    process.exit();
});

function algorila() {
    let problems = []
    input.forEach(cmd => {
        if (cmd == '문제')
            problems.push(cmd)
        else if (cmd == '고무오리' && problems.length == 0) {
            problems.push('문제')
            problems.push('문제')
        }
        else if (cmd == '고무오리')
            problems.pop()
    })

    if (problems.length == 0)
        console.log('고무오리야 사랑해')
    else
        console.log('힝구')
}
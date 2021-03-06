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
    let start = -1000000001, end = -1000000001
    let total = 0

    input = input.sort( (a, b) => {
        comp = parseInt(a.split(' ')[0]) - parseInt(b.split(' ')[0])
        if (comp == 0)
            comp = parseInt(a.split(' ')[1]) - parseInt(b.split(' ')[1])
        return comp
    })

    for (let idx = 0; idx < inputNum; idx++) {
        let curStart = parseInt(input[idx].split(' ')[0])
        let curEnd = parseInt(input[idx].split(' ')[1])

        if (curStart < end) {
            end = (curEnd > end) ? curEnd : end
        }
        else if (curStart >= end) {
            total += end - start
            start = curStart
            end = curEnd
        }
    }

    total += end - start

    console.log(total)
}

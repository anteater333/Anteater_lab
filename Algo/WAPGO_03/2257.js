/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = parseInt(line.split(' ')[0])
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
    // H : 1, C : 12, O : 16
    // ( : -1 (masking)
    let values = []
    let chemi = input.pop().split("")

    while (chemi.length > 0) {
        let char = chemi.shift()

        if (char == 'H') values.push(1)
        else if (char == 'C') values.push(12)
        else if (char == 'O') values.push(16)
        else if (char == '(') values.push(-1)
        else if (char == ')') {
            let subSum = 0
            while (values[values.length - 1] != -1 && values.length > 0) {
                subSum += values.pop()
            }
            // if (values.length == 0) {
            //     console.log("Wrong input")
            //     return
            // }
            values[values.length - 1] = subSum
        }
        else {  // 2 ~ 9
            // if (values.length == 0) {
            //     console.log("Wrong input")
            //     return
            // }
            values[values.length - 1] *= parseInt(char)
        }
    }

    let weight = values.reduce((pre, cur) => pre + cur)
    console.log(weight)
}
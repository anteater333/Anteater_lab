/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = parseInt(line.split(' ')[0]) + 1
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
    let expression = input.shift().split("")
    const varVal = input.map((val) => parseInt(val))

    expression = expression.map((char) => {
        let charCode = char.charCodeAt(0)
        if (65 <= charCode && charCode <= 90) {
            return varVal[charCode - 65]
        }
        return char
    })

    let postfixer = []
    while (expression.length > 0) {
        let op = expression.shift()
        let operand1 = 0
        let operand2 = 0

        if (op == '+') {
            operand2 = postfixer.pop()
            operand1 = postfixer.pop()
            postfixer.push(operand1 + operand2)
        }
        else if (op == '-') {
            operand2 = postfixer.pop()
            operand1 = postfixer.pop()
            postfixer.push(operand1 - operand2)
        }
        else if (op == '*') {
            operand2 = postfixer.pop()
            operand1 = postfixer.pop()
            postfixer.push(operand1 * operand2)
        }
        else if (op == '/') {
            operand2 = postfixer.pop()
            operand1 = postfixer.pop()
            postfixer.push(operand1 / operand2)
        }
        else {
            postfixer.push(op)
        }
    }

    console.log(postfixer[0].toFixed(2))
}
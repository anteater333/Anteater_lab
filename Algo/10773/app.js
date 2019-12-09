const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 0; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == 0) inputNum = parseInt(line);
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    let values = [];
    let sum = 0;

    input.forEach(value => {
        if (value != 0) values.push(parseInt(value));
        else values.pop();
    });

    values.forEach(value => {
        sum += value;
    });

    console.log(sum);
}
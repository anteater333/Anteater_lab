const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    const N = parseInt(input[0].split(" ")[0])
    const X = parseInt(input[0].split(" ")[1])

    input[1].split(" ").forEach(num => {
        if (parseInt(num) < X)
            process.stdout.write(num + " ")
    });
}
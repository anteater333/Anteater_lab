const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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
    const sugar3KG = 3;
    const sugar5KG = 5;

    let sugarYesPlease = parseInt(input.pop());   // 3 <= N <= 5000

    for (let i = parseInt(sugarYesPlease / sugar5KG); i >= 0; i--) {
        if (sugar5KG * i == sugarYesPlease) {   
            console.log(i);
            return;
        } else if (sugarYesPlease - sugar5KG * i < sugar3KG || (sugarYesPlease - sugar5KG * i) % sugar3KG != 0) {
            continue;
        } else {
            console.log(i + ((sugarYesPlease - sugar5KG * i) / sugar3KG));
            return;
        }
    }

    console.log(-1);
}
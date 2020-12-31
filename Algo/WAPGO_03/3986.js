/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

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
    let builder = []
    let gwCount = 0
    
    input.forEach(word => {
        builder = []
        let characters = word.split("")

        for (let index = 0; index < characters.length; index++) {
            let ch = characters[index]

            if (builder.length == 0) {
                builder.push(ch)
            }
            else if (builder[builder.length - 1] == ch) {
                builder.pop()
            }
            else {
                builder.push(ch)
            }
        }

        if (builder.length == 0)
            gwCount++;
    })
    console.log(gwCount)
}
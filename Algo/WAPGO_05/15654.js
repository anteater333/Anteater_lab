/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

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

let N, M
let selected
let sequences = []
let numbers = []

function algorila() {
    N = parseInt(input[0].split(' ')[0])
    M = parseInt(input[0].split(' ')[1])

    numbers = input[1].split(' ').map(a => parseInt(a)).sort((a, b) => a - b)

    selected = Array(N).fill(false)
    DFS('', 0)

    sequences.forEach( seq => console.log(seq) )
}

function DFS(sequence, depth) {
    if (depth == M) {
        sequences.push(sequence)
        return
    }

    for (let i = 0; i < N; i++) {
        if (selected[i])
            continue
        
        selected[i] = true
        DFS(sequence + numbers[i] + ' ', depth + 1)
        selected[i] = false
    }
}
/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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

function algorila() {
    N = parseInt(input[0].split(' ')[0])
    M = parseInt(input[0].split(' ')[1])

    selected = Array(N).fill(false)
    DFS('', 0, 0)

    sequences.forEach( seq => console.log(seq))
}

function DFS(sequence, depth, last) {
    if (depth == M) {
        sequences.push(sequence)
        return
    }

    for (let i = last; i < N; i++) {
        if (selected[i])
            continue
        
        selected[i] = true
        DFS(sequence + (i + 1) + ' ', depth + 1, i + 1)
        selected[i] = false
    }
}
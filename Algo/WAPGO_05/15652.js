/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

let N, M

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = line
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    N = parseInt(input[0].split(' ')[0])
    M = parseInt(input[0].split(' ')[1])
    DFS('', 0, 0)

    process.exit();
});

function DFS(sequence, depth, last) {
    for (let i = last; i < N; i++) {
        if (depth + 1 == M) 
            console.log(sequence + (i + 1))
        else 
            DFS(sequence + (i + 1) + ' ', depth + 1, i)
    }
}
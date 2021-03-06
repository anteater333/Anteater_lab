/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let maze = []

let N, M

rl.on('line', function(line) {
    if (inputNum == -1) {
        line = line.split(' ')
        N = inputNum = parseInt(line[0])
        M = parseInt(line[1])
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

let tovisit = []

function algorila() {
    input.forEach(line => {
        maze.push(line.split(''))
    });
    
    console.log(breadth_first_search(0, 0, N, M))
}

let drX = [-1, 0, 1, 0]
let drY = [0, -1, 0, 1]

function breadth_first_search(i, j, N, M) {
    tovisit.push([i, j, 1])

    while (tovisit.length != 0) {
        let curr = tovisit.shift()

        if (curr[0] == N - 1 && curr[1] == M - 1) {
            return curr[2]
        }

        maze[curr[0]][curr[1]] = -1
        for (let idx = 0; idx < 4; idx++) {
            let next = [curr[0] + drX[idx], curr[1] + drY[idx], curr[2] + 1]
            if (next[0] < 0 || next[1] < 0 || next[0] >= N || next[1] >= M)
                continue
            if (maze[next[0]][next[1]] != 1)
                continue
            tovisit.push(next)
        }
    }
}
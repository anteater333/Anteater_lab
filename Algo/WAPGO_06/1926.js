/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let inputNum = -1; // 입력 갯수
let paper = [];
let paintings = [];
let N, M;

rl.on('line', function(line) {
    if (inputNum == -1) {
        line = line.split(' ')
        inputNum = N = parseInt(line[0])
        M = parseInt(line[1])
    }
    else {
        paper.push(line.split(' '))
        if (paper.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    for (let i = 0; i < N; i++) {
        for (let j = 0; j < M; j++) {
            if (paper[i][j] == 1) {
                paintings.push(BFS(i, j))
            }
        }
    }

    console.log(paintings.length);
    if (paintings.length == 0) paintings.push(0)
    console.log(Math.max(...paintings))

    process.exit();
});

let tovisit = []

let drX = [0, -1, 0, 1]
let drY = [-1, 0, 1, 0]

function BFS(x, y) {
    let size = 0
    
    tovisit.push([x, y])
    paper[x][y] = 0

    let curr;
    let next;
    while (tovisit.length != 0) {
        curr = tovisit.shift()

        size++
        for (let idx = 0; idx < 4; idx++) {
            next = [curr[0] + drX[idx], curr[1] + drY[idx]]

            if (next[0] < 0 || next[1] < 0 || next[0] >= N || next[1] >= M)
                continue;
            if (paper[next[0]][next[1]] == 1) {
                tovisit.push(next)
                paper[next[0]][next[1]] = 0
            }
        }
    }

    return size
}
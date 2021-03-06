/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let graph = []
let tovisit = []

rl.on('line', function(line) {
    if (inputNum == -1) {
        inputNum = parseInt(line)
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
    let clusters = []

    input.forEach(line => {
        graph.push(line.split(''))
    });

    // searching, -1 : visited
    for (let i = 0; i < inputNum; i++) {
        for (let j = 0; j < inputNum; j++) {
            if (graph[i][j] == 0)
                graph[i][j] = -1
            else if (graph[i][j] == 1) {
                clusters.push(breadth_first_search(i, j))
            }
        }
    }

    console.log(clusters.length)
    clusters.sort((a, b) => a - b).forEach(cluster => console.log(cluster))
}

let drX = [-1, 0, 1, 0]
let drY = [0, -1, 0, 1]

function breadth_first_search(i, j) {
    let result = 0
    tovisit.push([i, j])

    while (tovisit.length != 0) {
        let curr = tovisit.shift()

        if (curr[0] < 0 || curr[1] < 0 || curr[0] >= inputNum || curr[1] >= inputNum)
            continue
        if (graph[curr[0]][curr[1]] != 1)
            continue

        graph[curr[0]][curr[1]] = -1
        result++
        for (let idx = 0; idx < 4; idx++) {
            tovisit.push([curr[0] + drX[idx], curr[1] + drY[idx]])
        }
    }
    
    return result;
}
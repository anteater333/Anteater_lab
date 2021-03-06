/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let graph = []

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
                clusters.push(depth_first_search(i, j))
            }
        }
    }

    console.log(clusters.length)
    clusters.sort((a, b) => a - b).forEach(cluster => console.log(cluster))
}

function depth_first_search(i, j) {
    if (i < 0 || j < 0 || i >= inputNum || j >= inputNum)
        return 0

    if (graph[i][j] == 0 || graph[i][j] == -1)
        return 0
    else {
        graph[i][j] = -1
        return depth_first_search(i - 1, j) + depth_first_search(i, j - 1) + depth_first_search(i + 1, j) + depth_first_search(i, j + 1) + 1
    }
}
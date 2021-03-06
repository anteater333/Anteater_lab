/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let numOfCom = 0

let graph
let visited

rl.on('line', function(line) {
    if (inputNum == -1) {
        numOfCom = parseInt(line)
        inputNum = 0

        graph = Array(numOfCom).fill(null).map(() => Array(numOfCom).fill(false))
        visited = Array(numOfCom).fill(false)
    }
    else if (inputNum == 0) {
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

    // building graph
    input.forEach(edge => {
        let v = edge.split(' ').map( v => parseInt(v) - 1)
        graph[v[0]][v[1]] = true
        graph[v[1]][v[0]] = true
    })

    console.log(depth_first_search(0) - 1)
}

function depth_first_search(vertex) {
    let cntSearched = 1
    visited[vertex] = true

    for (let idx = 0; idx < numOfCom; idx++) {
        if (idx == vertex)
            continue;
        if (visited[idx])
            continue;
        if (graph[vertex][idx]) {
            cntSearched += depth_first_search(idx)
        }
    }

    return cntSearched
}
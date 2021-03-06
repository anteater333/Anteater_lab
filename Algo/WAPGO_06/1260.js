/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let N
let start

let graph
let visited
let tovisit = []

rl.on('line', function(line) {
    if (inputNum == -1) {
        line = line.split(' ')
        N = parseInt(line[0])
        inputNum = parseInt(line[1])
        start = parseInt(line[2]) - 1

        graph = Array(N).fill(null).map(() => Array(N).fill(false))
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

let orderDFS = []
let orderBFS = []

function algorila() {

    // building graph
    input.forEach(edge => {
        let v = edge.split(' ').map( v => parseInt(v) - 1)
        graph[v[0]][v[1]] = true
        graph[v[1]][v[0]] = true
    })
    
    visited = Array(N).fill(false)
    depth_first_search(start)
    let stringDFS = ""
    orderDFS.map(v => { stringDFS += (v + 1) + " " })

    visited = Array(N).fill(false)
    breadth_first_search(start)
    let stringBFS = ""
    orderBFS.map(v => { stringBFS += (v + 1) + " " })

    console.log(stringDFS);
    console.log(stringBFS);
}

function depth_first_search(vertex) {
    orderDFS.push(vertex)
    visited[vertex] = true

    for (let idx = 0; idx < N; idx++) {
        if (idx == vertex)
            continue;
        if (visited[idx])
            continue;
        if (graph[vertex][idx]) {
            depth_first_search(idx)
        }
    }
}

function breadth_first_search(start) {
    tovisit.push(start)
    visited[start] = true

    let curr

    while(tovisit.length != 0) {
        curr = tovisit.shift()
        orderBFS.push(curr)
        for (let idx = 0; idx < N; idx++) {
            if (idx == curr)
                continue;
            if (visited[idx])
                continue;
            if (graph[curr][idx]) {
                tovisit.push(idx)
                visited[idx] = true
            }
        }
    }
}
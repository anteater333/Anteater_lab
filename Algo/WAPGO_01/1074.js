/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    const N = parseInt(input[0].split(" ")[0])
    const r = parseInt(input[0].split(" ")[1])
    const c = parseInt(input[0].split(" ")[2])

    console.log(Z(N, r, c))
}

function Z(N, r, c) {
    let half = Math.pow(2, N - 1)
    if ( r < half && c < half ) {   // 1사분면
        if (N == 1) return 0
        return Z(N - 1, r, c)
    }
    else if ( r < half && c >= half) {    // 2사분면
        if (N == 1) return 1
        return Z(N - 1, r, c - half) + half * half * 1
    }
    else if ( r >= half && c < half) {    // 3사분면
        if (N == 1) return 2
        return Z(N - 1, r - half, c) + half * half * 2
    }
    else if ( r >= half && c >= half) {    // 4사분면
        if (N == 1) return 3
        return Z(N - 1, r - half, c - half) + half * half * 3
    }
}
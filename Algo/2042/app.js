const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 0; // 입력 갯수

let N, M, K;

rl.on('line', function(line) {
    if (inputNum == 0) {
        const NMK = line.split(" ")
        N = parseInt(NMK[0])
        M = parseInt(NMK[1])
        K = parseInt(NMK[2])
        inputNum = N + M + K;
    }
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

function build(tree, size) {
    for (let i = size - 1; i > 0; i--) {
        if (i % 2 == 0)
            tree[i] = tree[i * 2] + tree[i * 2 + 1]
        else
            tree[i] = tree[i * 2] + tree[i * 2]
    }
}

function sum(tree, left, right, size) {
    let ans = 0
    for (left += size, right += size; left < right; left = Math.floor(left/2), right = Math.floor(right/2)) {
        if (left % 2 != 0)
            ans += tree[left++]
        if (right % 2 != 0)
            ans += tree[--right]
    }
    return ans
}

function update(tree, i, val, size) {
    for(tree[i += size] = val; i > 1; i = Math.floor(i/2)) {
        if (i % 2 == 0)
            tree[Math.floor(i/2)] = tree[i] + tree[i + 1]
        else 
            tree[Math.floor(i/2)] = tree[i] + tree[i - 1]
    }
}

function algorila() {
    let h = Math.ceil(Math.log2(N))
    let size = 2**(h+1)
    let treeL = size * 2
    let tree = Array(treeL).fill(0)

    for (let i = 0; i < N; i++) {
        tree[size + i] = parseInt(input.shift())
    }

    build(tree, size)

    for (let i = 0; i < input.length; i++ ) {
        let condition = input[i].split(" ").map(e => parseInt(e))

        if (condition[0] == 1) {    // change
            update(tree, condition[1] - 1, condition[2], size)
        }
        else if (condition[0] == 2) {   // sum
            console.log(sum(tree, condition[1] - 1, condition[2], size))
        }
    }
}

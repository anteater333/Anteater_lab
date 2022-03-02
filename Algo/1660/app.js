/* node.js */

const rl = require("readline").createInterface({
    input: process.stdin,
    output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on("line", function (line) {
    if (inputNum == -1) {
        inputNum = line;
    } else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on("close", function () {
    algorila();

    process.exit();
});

function algorila() {
    const N = +input[0];
    const balls = [1];
    const triangles = [1];

    let x = 2;
    while (balls[balls.length - 1] <= N) {
        triangles[triangles.length] = (x + x ** 2) / 2;
        balls[balls.length] = triangles.reduce((acc, cur) => acc + cur, 0);
        x++;
    }

    const memo = Array.from({ length: N + 1 }, () => Infinity);
    memo[0] = 0;

    for (let i = 0; i < balls.length; i++) {
        for (let j = balls[i]; j <= N; j++) {
            memo[j] = Math.min(memo[j], memo[j - balls[i]] + 1);
        }
    }
    console.log(memo[N]);
}

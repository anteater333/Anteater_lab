/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

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

const memo = [0, 1, 1, 1, 2, 2, 3, 4, 5];
function algorila() {
  input.forEach((TC) => {
    const N = +TC;
    for (let i = memo.length; i <= N; i++) {
      memo[i] = memo[i - 1] + memo[i - 5];
    }

    console.log(memo[N]);
  });
}

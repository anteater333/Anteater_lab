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
  const memo = [0, 1, 2];
  for (let i = 3; i <= N; i++) {
    memo[i] = (memo[i - 1] + memo[i - 2]) % 15746;
  }

  console.log(memo[N]);
}

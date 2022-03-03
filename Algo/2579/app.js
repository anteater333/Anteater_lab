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

function algorila() {
  const N = +inputNum;
  const stairs = [0, ...input.map((a) => +a)];
  const memo = [0, stairs[1], stairs[1] + stairs[2]];

  for (let i = 3; i < stairs.length; i++) {
    memo[i] = Math.max(memo[i - 2], stairs[i - 1] + memo[i - 3]) + stairs[i];
  }

  console.log(memo[N]);
}

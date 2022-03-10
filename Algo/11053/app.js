/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

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
  const arr = input[1].split(" ").map((a) => +a);
  const memo = Array.from({ length: N }, () => 0);

  for (let i = 0; i < N; i++) {
    memo[i] = 1;
    for (let j = 0; j < i; j++) {
      if (arr[i] > arr[j]) memo[i] = Math.max(memo[i], memo[j] + 1);
    }
  }

  console.log(Math.max(...memo));
}

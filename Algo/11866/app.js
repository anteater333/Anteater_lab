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
  const [N, K] = input
    .pop()
    .split(" ")
    .map((n) => parseInt(n));

  const ONE = Array.from(Array(N).keys());

  const buffer = [];

  let removedCnt = ONE.length;
  let index = -1;
  let kCnt = 1;
  while (removedCnt) {
    index++;
    if (index == ONE.length) {
      index = 0;
    }
    if (ONE[index] == -1) {
      continue;
    }
    if (!(kCnt % K)) {
      buffer[buffer.length] = ONE[index] + 1;
      ONE[index] = -1;
      removedCnt--;
    }
    kCnt++;
  }

  process.stdout.write("<");
  for (let i = 0; i < buffer.length - 1; i++) {
    process.stdout.write(buffer[i] + ", ");
  }
  process.stdout.write(buffer[buffer.length - 1] + ">\n");
}

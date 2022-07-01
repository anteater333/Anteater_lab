/* node.js */

// 15059 Hard choice

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
  const available = input[0].split(" ").map((a) => +a);
  const requested = input[1].split(" ").map((a) => +a);

  const c = Math.max(-available[0] + requested[0], 0);
  const b = Math.max(-available[1] + requested[1], 0);
  const p = Math.max(-available[2] + requested[2], 0);

  console.log(c + b + p);
}

/* node.js */

// 19602 Dog Treats

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 3; // 입력 갯수

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
  const S = +input[0];
  const M = +input[1];
  const L = +input[2];

  const formula = 1 * S + 2 * M + 3 * L;

  console.log(formula >= 10 ? "happy" : "sad");
}

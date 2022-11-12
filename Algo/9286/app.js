/* node.js */

// 9286 Gradabase

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let inputBuffer = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let M = -1;

let tCount = 0;
const outputBuffer = [];

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = +line;
  } else if (M == -1) {
    M = +line;
  } else if (M > 0) {
    inputBuffer.push(+line);
    M -= 1;
    if (M == 0) {
      outputBuffer.push(`Case ${++tCount}:`);
      algorila(inputBuffer);
      M = -1;
      inputNum -= 1;
      inputBuffer = [];
    }
  }
  if (inputNum == 0) {
    rl.close();
  }
}).on("close", function () {
  console.log(outputBuffer.join("\n"));
  process.exit();
});

function algorila(input) {
  const moved = input.filter((val) => val < 6).map((val) => val + 1);
  if (moved.length) outputBuffer.push(`${moved.join("\n")}`);
}

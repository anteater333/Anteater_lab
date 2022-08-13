/* node.js */

// 20254 Site Score

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
  const [Ur, Tr, Uo, To] = input[0].split(" ").map((a) => +a);

  console.log(56 * Ur + 24 * Tr + 14 * Uo + 6 * To);
}

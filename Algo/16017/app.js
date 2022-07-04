/* node.js */

// 16017 Telemarketer or not?

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 4; // 입력 갯수

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
  const digit = input.map((a) => +a);

  let isTelemarketer =
    (digit[0] === 8 || digit[0] === 9) &&
    digit[1] === digit[2] &&
    (digit[3] === 8 || digit[3] === 9);

  console.log(isTelemarketer ? "ignore" : "answer");
}

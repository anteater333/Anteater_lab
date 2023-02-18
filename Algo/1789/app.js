/* node.js */

// 1789 수들의 합

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐
/** @type {number} */
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
  const S = BigInt(+input[0]);

  let sum = 0;
  let cnt = 0;
  for (let i = 1; ; i++) {
    if (sum > S) {
      break;
    }
    sum += i;
    cnt++;
  }

  console.log(cnt - 1);
}

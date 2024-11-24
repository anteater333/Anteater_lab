/* node.js */

// 1598 꼬리를 무는 숫자 나열

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
let inputNum = 1; // 입력 개수

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
  const [a, b] = input[0].split(" ").map((n) => +n);

  const X = Math.abs(Math.floor((a - 1) / 4) - Math.floor((b - 1) / 4));
  const Y = Math.abs(((a - 1) % 4) - ((b - 1) % 4));

  // console.log(Math.floor((a - 1) / 4), Math.floor((b - 1) / 4));

  // console.log(Math.abs((a - 1) % 4), Math.abs((b - 1) % 4));

  console.log(X + Y);
}

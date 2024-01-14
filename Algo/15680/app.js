/* node.js */

// 15680 연세대학교

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
  const N = +input[0];

  if (N === 0) {
    console.log("YONSEI");
  } else {
    console.log("Leading the Way to the Future");
  }
}

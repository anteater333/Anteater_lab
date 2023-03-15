/* node.js */

// 1193 분수찾기

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
  let n = +input[0];
  let line = 1;

  while (line < n) {
    n -= line;
    line++;
  }

  let top, bottom;
  if (line % 2 === 0) {
    top = n;
    bottom = line - n + 1;
  } else {
    top = line - n + 1;
    bottom = n;
  }

  console.log(`${top}/${bottom}`);
}

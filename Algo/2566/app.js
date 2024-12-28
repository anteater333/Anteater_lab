/* node.js */

// 2566 최댓값

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
let inputNum = 9; // 입력 개수

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
  let max = 0;
  let maxRow = 1;
  let maxCol = 1;

  for (let i = 0; i < inputNum; i++) {
    const row = input[i].split(" ").map(Number);
    for (let j = 0; j < 9; j++) {
      if (row[j] > max) {
        max = row[j];
        maxRow = i + 1;
        maxCol = j + 1;
      }
    }
  }

  console.log(max);
  console.log(maxRow, maxCol);
}


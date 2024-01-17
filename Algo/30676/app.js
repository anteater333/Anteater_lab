/* node.js */

// 30676 이 별은 무슨 색일까

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
  const value = +input[0];

  let color = "";
  if (620 <= value && value <= 780) {
    color = "Red";
  } else if (590 <= value) {
    color = "Orange";
  } else if (570 <= value) {
    color = "Yellow";
  } else if (495 <= value) {
    color = "Green";
  } else if (450 <= value) {
    color = "Blue";
  } else if (425 <= value) {
    color = "Indigo";
  } else if (380 <= value) {
    color = "Violet";
  }

  console.log(color);
}

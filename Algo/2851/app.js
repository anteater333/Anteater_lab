/* node.js */

// 2851 슈퍼 마리오

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
let inputNum = 10; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = line;
  } else {
    input.push(+line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  let sum = 0;
  /** 현재 점수의 100점과의 차이 */
  let currDiff = 100;
  for (const mush of input) {
    /** 다음 버섯을 먹었을 때의 100점과의 차이 */
    const nextDiff = Math.abs(100 - (sum + mush));

    // 차이가 더 늘어남
    if (currDiff < nextDiff) {
      break;
    }

    // 차이가 더 줄어듬, 다음 버섯으로
    sum += mush;
    currDiff = nextDiff;
  }

  console.log(sum);
}

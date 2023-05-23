/* node.js */

// 1654 랜선 자르기

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
let inputNum = -1; // 입력 개수

let K, N;

rl.on("line", function (line) {
  if (inputNum == -1) {
    [K, N] = line.split(" ").map((a) => +a);
    inputNum = K;
  } else {
    input.push(+line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  let [min, max] = [1, Math.max(...input)];

  let notDone = true;
  let maxLen = 0;

  while (notDone) {
    let curLen = Math.floor((max + 1 - min) / 2) + min;
    let sum = 0;
    for (let i = 0; i < K; i++) {
      sum += Math.floor(input[i] / curLen);
    }

    if (sum < N) {
      // 현재 길이에서는 달성 실패, 더 짧게
      max = curLen - 1;
    } else {
      // 더 길게도 가능한지 확인
      min = curLen + 1;
      maxLen = Math.max(curLen, maxLen);
    }

    notDone = max - min >= 0;
  }

  console.log(maxLen);
}

/* node.js */

// 1236 성 지키기

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
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = +line.split(" ")[0];
  } else {
    input.push(line.split(""));
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const N = input.length;
  const M = input[0].length;

  let securityX = 0;
  let securityY = 0;

  for (let i = 0; i < N; i++) {
    for (let j = 0; j < M; j++) {
      if (input[i][j] === "X") {
        securityX++;
        break;
      }
    }
  }

  for (let j = 0; j < M; j++) {
    for (let i = 0; i < N; i++) {
      if (input[i][j] === "X") {
        securityY++;
        break;
      }
    }
  }

  console.log(Math.max(N - securityX, M - securityY));
}

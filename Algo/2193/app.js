/* node.js */

// 2193 이친수

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let N = 0; // 입력
/** @type {number} */
let inputNum = 1; // 입력 갯수

rl.on("line", function (line) {
  N = +line;
  rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

const pinaries = [1, 1, 2];

function algorila() {
  for (let i = 3; i <= N - 1; i++) {
    pinaries[i] = BigInt(2) * BigInt(pinaries[i - 2]) + BigInt(pinaries[i - 3]);
  }

  console.log(pinaries[N - 1].toString());
}

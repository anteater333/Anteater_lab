/* node.js */

// 1769 3의 배수

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
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  let X = input[0];

  let cnt = 0;
  while (X.length > 1) {
    X = X.split("")
      .map((a) => +a)
      .reduce((pv, cv) => pv + cv, 0)
      .toString();
    cnt++;
  }

  console.log(cnt);
  console.log(X % 3 === 0 ? "YES" : "NO");
}

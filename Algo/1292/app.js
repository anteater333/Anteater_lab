/* node.js */

// 1292 쉽게 푸는 문제

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
  const [A, B] = input[0].split(" ").map((a) => +a);

  let target = 1;
  let counter = 0;

  for (let i = 0; i < A - 1; i++) {
    counter++;
    if (target === counter) {
      target++;
      counter = 0;
    }
  }

  let sum = 0;

  for (let i = A - 1; i < B; i++) {
    sum += target;

    counter++;
    if (target === counter) {
      target++;
      counter = 0;
    }
  }

  console.log(sum);
}

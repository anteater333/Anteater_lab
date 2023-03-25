/* node.js */

// 2563 색종이

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
    inputNum = +line;
  } else {
    input.push(line.split(" ").map((a) => +a));
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const field = [...Array(100)].map((e) => Array(100).fill(0));

  input.forEach((line) => {
    const [x, y] = line.map((a) => a - 1);

    for (let i = x; i < x + 10; i++) {
      for (let j = y; j < y + 10; j++) {
        field[i][j] = 1;
      }
    }
  });

  let cnt = 0;
  for (let i = 0; i < 100; i++) {
    for (let j = 0; j < 100; j++) {
      if (field[i][j] === 1) cnt++;
    }
  }
  console.log(cnt);
}

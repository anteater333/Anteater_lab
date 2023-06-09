/* node.js */

// 5575 타임 카드

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
let inputNum = 3; // 입력 개수

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
  for (let i = 0; i < 3; i++) {
    const numbers = input[i].split(" ").map((a) => +a);

    const start = time2Sec(numbers[0], numbers[1], numbers[2]);
    const end = time2Sec(numbers[3], numbers[4], numbers[5]);

    console.log(sec2Time(end - start));
  }
}

function time2Sec(h, m, s) {
  return s + 60 * m + 3600 * h;
}

function sec2Time(sec) {
  const h = Math.floor(sec / 3600);
  const m = Math.floor((sec % 3600) / 60);
  const s = sec % 60;

  return `${h} ${m} ${s}`;
}

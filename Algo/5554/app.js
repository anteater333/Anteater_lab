/* node.js */

// 5554 심부름 가는 길

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
let inputNum = 4; // 입력 개수

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
  const [toSchool, toPCBang, toHakwon, toHome] = input.map((a) => +a);

  const total = toSchool + toPCBang + toHakwon + toHome;

  console.log(Math.floor(total / 60));
  console.log(total % 60);
}

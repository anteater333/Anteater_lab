/* node.js */

// 25304 영수증

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
let inputNum = -2; // 입력 갯수

let receipt = 0;

rl.on("line", function (line) {
  if (inputNum == -2) {
    receipt = +line;
    inputNum++;
  } else if (inputNum == -1) {
    inputNum = +line;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  let sum = 0;
  input.forEach((item) => {
    const [cost, count] = item.split(" ").map((a) => +a);
    sum += cost * count;
  });

  console.log(receipt === sum ? "Yes" : "No");
}

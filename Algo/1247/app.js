/* node.js */

// 1247 부호

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
let N = 0;
let subN = -1;

const output = [];

rl.on("line", function (line) {
  if (subN === -1) {
    subN = +line;
    return;
  } else {
    input.push(line);
  }

  if (input.length === subN) {
    algorila();
    N++;
    subN = -1;
    input = [];
  }

  if (N === inputNum) rl.close();
}).on("close", function () {
  console.log(output.join("\n"));

  process.exit();
});

function algorila() {
  let sum = BigInt(0);

  input.forEach((el) => {
    sum += BigInt(el);
  });

  output.push(sum === BigInt(0) ? "0" : sum > 0 ? "+" : "-");
}

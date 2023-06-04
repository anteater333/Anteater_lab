/* node.js */

// 1267 핸드폰 요금

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
let inputNum = 2; // 입력 개수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const call = input[1].split(" ").map((a) => +a);

  let Y = 0;
  let M = 0;
  call.forEach((c) => {
    Y += (Math.floor(c / 30) + 1) * 10;
    M += (Math.floor(c / 60) + 1) * 15;
  });

  console.log(Y < M ? "Y" : Y === M ? "Y M" : "M", Math.min(Y, M));
}

/* node.js */

// 14581 팬들에게 둘러싸인 홍준

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
  const f = ":fan:";
  const h = `:${input[0]}:`;

  console.log(`${f}${f}${f}`);
  console.log(`${f}${h}${f}`);
  console.log(`${f}${f}${f}`);
}

/* node.js */

// 2480 주사위 세개

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
  const dices = input[0].split(" ").map((a) => +a);

  let prize = 0;
  if (dices[0] === dices[1] && dices[1] === dices[2]) {
    prize = dices[0] * 1000 + 10000;
  } else if (dices[0] === dices[1]) {
    prize = dices[0] * 100 + 1000;
  } else if (dices[0] === dices[2]) {
    prize = dices[0] * 100 + 1000;
  } else if (dices[1] === dices[2]) {
    prize = dices[1] * 100 + 1000;
  } else {
    prize = Math.max(...dices) * 100;
  }

  console.log(prize);
}

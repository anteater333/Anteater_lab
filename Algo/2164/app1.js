/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
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
  const cards = Array.from(Array(parseInt(input[0])).keys());

  let abandon = true;
  console.time("time");
  while (cards.length > 1) {
    if (abandon) {
      cards.shift();
    } else {
      cards[cards.length - 1] = cards.shift();
    }
    abandon = !abandon;
  }
  console.timeEnd("time");

  process.stdout.write(`${cards[0] + 1}\n`);
}

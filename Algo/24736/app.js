/* node.js */

// 24736 Football Scoring

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

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
  input.forEach((teamScore) => {
    const [T, F, S, P, C] = teamScore.split(" ").map((a) => +a);
    process.stdout.write(`${6 * T + 3 * F + 2 * S + 1 * P + 2 * C} `);
  });
  console.log("");
}

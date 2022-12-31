/* node.js */

// 10039 평균 점수

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 5; // 입력 갯수

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
  let sum = 0;
  input.map((a) => {
    const score = +a;

    sum += score >= 40 ? score : 40;
  });

  console.log(Math.round(sum / input.length));
}

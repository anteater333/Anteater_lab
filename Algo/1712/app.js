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
  const [A, B, C] = input[0].split(" ").map((a) => +a);

  if (B >= C) {
    // 노트북 하나 만드는 비용 >= 노트북 하나 파는 비용
    // 손익분기점 존재 X
    console.log(-1);
    return;
  }

  console.log(Math.floor(A / (C - B)) + 1);
}

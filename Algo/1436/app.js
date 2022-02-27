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
  const N = +input[0];

  let doomNumCnt = 0;
  let unjudgedNum = 666;
  while (doomNumCnt < N) {
    if (unjudgedNum.toString().includes("666")) {
      doomNumCnt++;
    }
    unjudgedNum++;
  }

  console.log(unjudgedNum - 1);
}

/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 9; // 입력 갯수

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
  const arr = input.map((a) => +a);
  let idx = 0;

  arr.forEach((element, jdx) => {
    if (arr[idx] < element) {
      idx = jdx;
    }
  });

  console.log(arr[idx]);
  console.log(idx + 1);
}

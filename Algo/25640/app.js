/* node.js */

// 25640 MBTI

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -2; // 입력 갯수
let JINHO = "";

rl.on("line", function (line) {
  if (inputNum == -2) {
    inputNum++;
    JINHO = line;
  } else if (inputNum == -1) {
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
  let count = 0;

  input.forEach((mbti) => {
    count += mbti === JINHO ? 1 : 0;
  });

  console.log(count);
}

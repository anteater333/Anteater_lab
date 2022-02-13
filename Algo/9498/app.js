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
  const point = +input[0];
  let grade = "F";

  if (point >= 90) {
    grade = "A";
  } else if (point >= 80) {
    grade = "B";
  } else if (point >= 70) {
    grade = "C";
  } else if (point >= 60) {
    grade = "D";
  }

  console.log(grade);
}

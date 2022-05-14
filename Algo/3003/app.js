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
  const units = [1, 1, 2, 2, 2, 8];
  const curUnits = input[0].split(" ").map((n) => +n);

  console.log(units.map((unit, index) => unit - curUnits[index]).join(" "));
}

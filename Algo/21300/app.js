/* node.js */

// 21300 Bottle Return

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

/**
 * beear, malt, wine, carbonated soft drins, seltzer, water
 */

function algorila() {
  console.log(input[0].split(" ").reduce((sum, cur) => sum + +cur, 0) * 5);
}

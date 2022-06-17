/* node.js */

// 6778 Which Alien?

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
  const antenna = +input[0];
  const eyes = +input[1];

  if (antenna >= 3 && eyes <= 4) {
    console.log("TroyMartian");
  }
  if (antenna <= 6 && eyes >= 2) {
    console.log("VladSaturnian");
  }
  if (antenna <= 2 && eyes <= 3) {
    console.log("GraemeMercurian");
  }
}

/* node.js */

// 1417 국회의원 선거

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

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
  const candies = input.map((a) => +a);
  let mindControl = 0;
  while (
    (() => {
      for (let i = 1; i < candies.length; i++) {
        if (candies[0] <= candies[i]) {
          return true;
        }
      }
      return false;
    })()
  ) {
    let maxIdx = 1;
    candies.map((candy, idx) => {
      if (idx == 0) return;
      if (candy > candies[maxIdx]) {
        maxIdx = idx;
      }
    });

    mindControl++;
    candies[0]++;
    candies[maxIdx]--;
  }
  console.log(mindControl);
}
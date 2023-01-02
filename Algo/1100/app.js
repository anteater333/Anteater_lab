/* node.js */

// 1100 하얀 칸

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 8; // 입력 갯수

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
  const board = input;

  let cnt = 0;
  let x = 0;

  board.forEach((line) => {
    for (let y = 0; y < line.length; y++) {
      const slot = line[y];

      cnt += (x + y) % 2 === 0 && slot === "F" ? 1 : 0;
    }
    x++;
  });

  console.log(cnt);
}

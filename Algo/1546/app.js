/* node.js */

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
  const N = +input[0];
  const points = input[1].split(" ").map((a) => +a);

  const M = Math.max(...points);

  points.forEach((point, idx) => {
    points[idx] = (point / M) * 100;
  });

  console.log(points.reduce((p, c) => (p += c), 0) / N);
}

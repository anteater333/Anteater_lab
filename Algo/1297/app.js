/* node.js */

// 1297 TV 크기

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
  const [D, H, W] = input[0].split(" ").map((a) => +a);

  const abstD = Math.sqrt(H ** 2 + W ** 2);

  const realH = (D * H) / abstD;
  const realW = (D * W) / abstD;

  console.log(Math.floor(realH), Math.floor(realW));
}

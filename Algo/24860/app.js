/* node.js */

// 24860 Counting Antibodies

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 3; // 입력 갯수

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
  const [Vk, Jk] = input[0].split(" ").map((a) => +a);
  const [Vl, Jl] = input[1].split(" ").map((a) => +a);
  const [Vh, Dh, Jh] = input[2].split(" ").map((a) => +a);

  const k = Vk * Jk;
  const l = Vl * Jl;
  const h = Vh * Dh * Jh;

  console.log(h * (k + l));
}

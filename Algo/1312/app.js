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
  const [A, B, N] = input[0].split(" ").map((a) => +a);

  /** 무식하게 풀어보기 */
  let x = A;
  let d;
  for (let i = 0; i <= N; i++) {
    d = Math.floor(x / B);
    const m = x % B;
    x = m * 10;
  }

  console.log(d);
}

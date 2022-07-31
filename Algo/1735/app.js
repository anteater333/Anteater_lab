/* node.js */

// 1735 분수 합

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
  const [A1, B1] = input[0].split(" ").map((a) => +a);
  const [A2, B2] = input[1].split(" ").map((a) => +a);

  const A3 = A1 * B2 + A2 * B1;
  const B3 = B1 * B2;
  const GCD = gcd(A3, B3);
  console.log(A3 / GCD, B3 / GCD);
}

function gcd(a, b) {
  let r;

  while (b) {
    r = a % b;
    a = b;
    b = r;
  }

  return a;
}

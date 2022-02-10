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
  const N = input[0]
    .split("")
    .map((a) => BigInt(a))
    .reverse();

  let NDec = 0n;
  for (let i = 0n; i < N.length; i++) {
    NDec += N[i] * 2n ** i;
  }

  console.log((NDec * 17n).toString(2));
}

/* node.js */
// 실패!

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
  const N = parseInt(input[0]);
  const mod = 1000000007n;

  const Ezreal = Array(N - 1);
  Ezreal[0] = [0n, 1n, 0n];

  for (let index = 1; index < N; index++) {
    const before = Ezreal[index - 1];
    Ezreal[index] = [];
    Ezreal[index][0] = before[1] + before[2];
    Ezreal[index][1] = before[0] + before[2];
    Ezreal[index][2] = before[0] + before[1];
  }

  console.log(Number(Ezreal[N - 1][0] % mod));
}

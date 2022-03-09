/* node.js */

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
  const N = +inputNum;

  const memo = Array.from({ length: N }, () => []);

  memo[0][0] = +input[0];

  let max = memo[0][0];

  for (let i = 1; i < N; i++) {
    const row = input[i].split(" ").map((a) => +a);
    for (let j = 0; j <= i; j++) {
      if (j == i) {
        memo[i][j] = row[j] + memo[i - 1][j - 1];
      } else if (j == 0) {
        memo[i][j] = row[j] + memo[i - 1][j];
      } else memo[i][j] = row[j] + Math.max(memo[i - 1][j - 1], memo[i - 1][j]);

      max = Math.max(memo[i][j], max);
    }
  }

  console.log(max);
}

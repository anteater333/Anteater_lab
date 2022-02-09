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
  /** memo[0] : 0의 호출 횟수, memo[1] : 1의 호출 횟수 */
  const memo = [
    [1, 0],
    [0, 1],
  ];

  const result = [];

  input.forEach((element) => {
    const N = +element;
    if (!memo[0][N]) {
      for (let i = memo[0].length; i <= N; i++) {
        memo[0][i] = memo[0][i - 1] + memo[0][i - 2];
        memo[1][i] = memo[1][i - 1] + memo[1][i - 2];
      }
    }
    result.push(`${memo[0][N]} ${memo[1][N]}`);
  });

  console.log(result.join("\n"));
}

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
  const [N, M] = input[0].split(" ").map((a) => +a);
  const primeGrid = new Array(M).fill(0).map((_, i, arr) => (arr[i] = i + 1));

  const primeNumbers = [];

  for (let i = 0; i < primeGrid.length; i++) {
    const curNum = primeGrid[i];
    if (curNum == 1) {
      // 낫 어 소수
      continue;
    }

    if (curNum >= N) {
      primeNumbers.push(curNum);
    }

    for (let j = i + curNum; j < primeGrid.length; j += curNum) {
      primeGrid[j] = 1;
    }
  }

  console.log(primeNumbers.join("\n"));
}

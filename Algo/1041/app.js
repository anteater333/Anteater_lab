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

function SumOfD1(N, D1) {
  return (5 * N ** 2 - 8 * N + 4) * D1;
}

function SumOfD2(N, D2) {
  return (8 * N - 8) * D2;
}

function SumOfD3(D3) {
  return 4 * D3;
}

function algorila() {
  /** N^3, 주사위의 수 */
  const N = parseInt(input[0]);
  /** 주사위 눈금 배열 */
  const dice = input[1].split(" ").map((a) => +a);
  /** 눈금합계 */
  let sum = 0;
  /** N == 1인 경우 예외 처리 */
  if (N == 1) {
    const biggestIndex = dice.findIndex((v) => v == Math.max(...dice));
    dice.forEach((value, index) => {
      if (index != biggestIndex) sum += value;
    });
  } else {
    /** 눈금 짝 묶음, length == 3 */
    const dicePair = [];
    for (let i = 0; i < 3; i++) {
      dicePair[dicePair.length] = [dice[i], dice[5 - i]].sort((a, b) => a - b);
    }
    /** 정렬한 눈금 짝 */
    const sortedDicePair = dicePair.sort((a, b) => {
      return a[0] - b[0];
    });

    const sumOfD1 = SumOfD1(N, sortedDicePair[0][0]);
    const sumOfD2 = SumOfD2(N, sortedDicePair[1][0]);
    const sumOfD3 = SumOfD3(sortedDicePair[2][0]);

    sum = sumOfD1 + sumOfD2 + sumOfD3;
  }

  console.log(sum);
  return sum;
}

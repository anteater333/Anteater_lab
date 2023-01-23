/* node.js */

// 2167 2차원 배열의 합

let N, M;
let K;
let map = [];

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -2; // 입력 갯수
let subInputNum = 0;

rl.on("line", function (line) {
  if (inputNum == -2) {
    inputNum = -1;
    NM = line.split(" ").map((a) => +a);
    N = NM[0];
    M = NM[1];
  } else if (inputNum == -1 && subInputNum != N) {
    subInputNum += 1;
    map.push(line);
  } else if (inputNum == -1 && subInputNum == N) {
    K = +line;
    inputNum = K;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  map = map.map((l) => l.split(" ").map((a) => +a));

  input.forEach((TC) => {
    const [i, j, x, y] = TC.split(" ").map((a) => +a - 1);

    let sum = 0;
    for (let indexI = i; indexI <= x; indexI++) {
      for (let indexJ = j; indexJ <= y; indexJ++) {
        sum += map[indexI][indexJ];
      }
    }

    console.log(sum);
  });
}

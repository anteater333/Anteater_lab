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

let count = 0;

function algorila() {
  const N = +input[0];

  putNQueens(0, N);
  console.log(count);
}

function putNQueens(depth, N, posArr = []) {
  if (depth == N) {
    count++;
    return;
  }

  if (depth == 0) {
    for (let i = 0; i < N; i++) {
      putNQueens(depth + 1, N, [i]);
    }
  } else {
    for (let i = 0; i < N; i++) {
      let promising = true;
      for (let j = 0; j < depth; j++) {
        if (Math.abs(i - posArr[j]) == Math.abs(depth - j)) {
          promising = false;
          break;
        }
        if (i == posArr[j]) {
          promising = false;
          break;
        }
      }
      if (promising) putNQueens(depth + 1, N, [...posArr, i]);
    }
  }
}

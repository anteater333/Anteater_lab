/* node.js */

// 2740 행렬 곱셈

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐
/** @type {number} */
let inputNum = -2; // 입력 갯수

let N, M, K;
let mat1 = [],
  mat2 = [];

rl.on("line", function (line) {
  if (N === undefined) {
    [N, M] = line.split(" ").map((a) => +a);
    return;
  }

  if (mat1.length < N) {
    mat1.push(line.split(" ").map((a) => +a));
    return;
  }

  if (mat1.length === N && K === undefined) {
    [M, K] = line.split(" ").map((a) => +a);
    return;
  }

  if (mat2.length < M && K !== undefined) {
    mat2.push(line.split(" ").map((a) => +a));
    if (mat2.length === M) {
      rl.close();
      return;
    }
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const resultMat = [...Array(mat1.length)].map((e) =>
    Array(mat2[0].length).fill(0)
  );

  for (let n = 0; n < mat1.length; n++) {
    for (let k = 0; k < mat2[0].length; k++) {
      let subSum = 0;

      for (let m = 0; m < mat2.length; m++) {
        subSum += mat1[n][m] * mat2[m][k];
      }

      resultMat[n][k] = subSum;
    }
  }

  console.log(resultMat.map((l) => l.join(" ")).join("\n"));
}

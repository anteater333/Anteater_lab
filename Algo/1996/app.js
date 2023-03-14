/* node.js */

// 1996 지뢰 찾기

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

  // 입력받은 지뢰 지도
  const hintMap = input.map((line) => line.split(""));

  // 결과를 기록할 지도
  const mineMap = [...Array(N)].map((e) => Array(N).fill(0));

  // 마킹 방향 결정자
  const markHelper = [
    [1, 0],
    [1, 1],
    [0, 1],
    [-1, 1],
    [-1, 0],
    [-1, -1],
    [0, -1],
    [1, -1],
  ];

  /**
   * x, y 좌표 기준 8방향으로 숫자 기록
   * @param {*} x
   * @param {*} y
   */
  const markMine = (x, y) => {
    mineMap[x][y] = "*";

    for (const [dx, dy] of markHelper) {
      const X = x + dx;
      const Y = y + dy;

      if (
        X < 0 ||
        X >= N ||
        Y < 0 ||
        Y >= N ||
        mineMap[X][Y] === "*" ||
        mineMap[X][Y] === "M"
      ) {
        continue;
      }

      mineMap[X][Y] += +hintMap[x][y];
      if (mineMap[X][Y] > 9) mineMap[X][Y] = "M";
    }
  };

  // 입력받은 지도 순회
  for (let i = 0; i < N; i++) {
    for (let j = 0; j < N; j++) {
      if (hintMap[i][j] !== ".") {
        markMine(i, j);
      }
    }
  }

  console.log(mineMap.map((line) => line.join("")).join("\n"));
}

/* node.js */

// 1388 바닥 장식

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

let N, M;

rl.on("line", function (line) {
  if (inputNum == -1) {
    [N, M] = line.split(" ").map((a) => +a);
    inputNum = N;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const floor = input.map((line) => line.split(""));

  let boardCnt = 0;
  for (let i = 0; i < N; i++) {
    for (let j = 0; j < M; j++) {
      if (floor[i][j] === "-" || floor[i][j] === "|") {
        markBoardLength(i, j, floor);
        boardCnt++;
      }
    }
  }

  console.log(boardCnt);
}

/**
 * DFS를 사용해 바닥 지도에 이어진 판자 부분을 표시
 */
function markBoardLength(x, y, floor) {
  const [N, M] = [floor.length, floor[0].length];
  const fTile = floor[x][y];
  if (fTile === ".") return; // escape
  const dx = fTile === "-" ? [0, 1] : [1, 0]; // direction

  const stackBuffer = [[x, y]];
  while (stackBuffer.length) {
    const [cX, cY] = stackBuffer.pop();

    if (cX >= N || cY >= M) continue;

    if (floor[cX][cY] === fTile) {
      floor[cX][cY] = ".";
      stackBuffer.push([cX + dx[0], cY + dx[1]]);
    }
  }
}

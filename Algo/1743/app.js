/* node.js */

// 1743 음식물 피하기

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {number} */
let inputCnt = 0; // 입력 받은 개수
/** @type {number} */
let inputNum = -1; // 입력 전체 개수

let N, M;

let hall;

rl.on("line", function (line) {
  if (inputNum == -1) {
    [N, M, inputNum] = line.split(" ").map((a) => +a);

    hall = [...Array(N)].map((_) => Array(M).fill(0));
  } else {
    const [x, y] = line.split(" ").map((a) => +a - 1);
    hall[x][y] = 1;

    inputCnt++;

    if (inputCnt == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  console.log(calcMaxRubbishSize(hall));
}

/**
 * 복도 지도를 통해 바닥에 떨어진 가장 큰 음식물 쓰레기의 크기를 계산
 * @param {Array<Array<number>>} map
 */
function calcMaxRubbishSize(map) {
  const [N, M] = [map.length, map[0].length];

  // DFS
  function DFS(x, y) {
    let dx = [
      [1, 0],
      [-1, 0],
      [0, 1],
      [0, -1],
    ];

    let stack = [];
    if (map[x][y] === 1) {
      stack.push([x, y]);
      map[x][y] = -1;
    }

    let size = stack.length;
    while (stack.length) {
      const [curX, curY] = stack.pop();

      for (let i = 0; i < 4; i++) {
        const [newX, newY] = [curX + dx[i][0], curY + dx[i][1]];

        if (newX >= N || newX < 0 || newY >= M || newY < 0) {
          continue;
        }

        if (map[newX][newY] === 1) {
          map[newX][newY] = -1;
          size++;
          stack.push([newX, newY]);
        }
      }
    }

    return size;
  }

  let maxSize = 0;
  for (let i = 0; i < N; i++) {
    for (let j = 0; j < M; j++) {
      if (map[i][j] === 1) {
        curSize = DFS(i, j);

        maxSize = curSize > maxSize ? curSize : maxSize;
      }
    }
  }

  return maxSize;
}

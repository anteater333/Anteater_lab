/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let M, N, V, X, Y;
/**
 * 지도
 */
const map = [];
/**
 * 용암 지도
 * 0 : 화산
 * 1~ : 용암이 닿게 되는 시각
 */
const magma = [];

rl.on("line", function (line) {
  if (inputNum == -1) {
    [M, N, V] = line.split(" ").map((a) => +a);
    inputNum = 1 + M + V;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  [X, Y] = input[0].split(" ").map((a) => +a - 1);
  for (let i = 1; i <= M; i++) {
    map[map.length] = input[i].split(" ").map((a) => +a);
    magma[magma.length] = new Array(N).fill(Infinity);
  }

  // 마그마 지도 생성
  for (let i = 1 + M; i <= M + V; i++) {
    const [x, y, t] = input[i].split(" ").map((a) => +a);
    magma[x - 1][y - 1] = 0;
    erupt(x - 1, y - 1, t);
  }

  // 경로 탐색
  console.log(...run(X, Y));
}

/**
 * 화산 분출 시뮬레이터
 * @param {+} x
 * @param {*} y
 * @param {*} t
 */
function erupt(x, y, t) {
  const areaQue = [[x, y]];
  let head = 0;
  const factor = [
    [0, 1],
    [1, 0],
    [0, -1],
    [-1, 0],
  ];
  const visited = [];
  for (let i = 0; i < M; i++) {
    visited[visited.length] = new Array(N).fill(false);
  }

  visited[x][y] = true;

  while (head < areaQue.length) {
    const cursor = areaQue[head++];

    magma[cursor[0]][cursor[1]] = Math.min(
      magma[cursor[0]][cursor[1]],
      t + (Math.abs(x - cursor[0]) + Math.abs(y - cursor[1]))
    );

    for (let i = 0; i < 4; i++) {
      const newX = cursor[0] + factor[i][0];
      const newY = cursor[1] + factor[i][1];
      if (
        !(newX < 0 || newX >= M || newY < 0 || newY >= N || visited[newX][newY])
      ) {
        visited[newX][newY] = true;
        areaQue[areaQue.length] = [newX, newY];
      }
    }
  }

  //   for (let i = 0; i < M; i++) {
  //     for (let j = 0; j < N; j++) {
  //       process.stdout.write(magma[i][j] + "\t");
  //     }
  //     console.log("");
  //   }
  //   console.log("========================");
}

/**
 * 도망쳐!
 *
 * @param {*} X
 * @param {*} Y
 */
function run(X, Y) {
  const areaQue = [[X, Y, 0]];
  let head = 0;
  const factor = [
    [0, 1],
    [1, 0],
    [0, -1],
    [-1, 0],
  ];
  const visited = [];
  for (let i = 0; i < M; i++) {
    visited[visited.length] = new Array(N).fill(false);
  }

  let tallestHeight = map[X][Y];
  let bestDistance = 0;

  visited[X][Y] = true;

  while (head < areaQue.length) {
    const cursor = areaQue[head++];

    const height = map[cursor[0]][cursor[1]];
    const distance = cursor[2];

    // console.log(cursor, distance, magma[cursor[0]][cursor[1]], height);
    if (distance >= magma[cursor[0]][cursor[1]]) {
      // 사망
      //   console.log("꾸엑");
      continue;
    }

    if (tallestHeight < height) {
      tallestHeight = height;
      bestDistance = distance;
    }

    for (let i = 0; i < 4; i++) {
      const newX = cursor[0] + factor[i][0];
      const newY = cursor[1] + factor[i][1];
      if (
        !(newX < 0 || newX >= M || newY < 0 || newY >= N || visited[newX][newY])
      ) {
        visited[newX][newY] = true;
        areaQue[areaQue.length] = [newX, newY, distance + 1];
      }
    }
  }

  return [tallestHeight, bestDistance];
}

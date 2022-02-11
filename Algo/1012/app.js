/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

const result = [];
let TC = -1; // 입력 갯수
let TCount = 0;

let M, N, K;
let cabbCount;
let cabbFarm;

// 입력 받는게 몬가 구림
rl.on("line", function (line) {
  if (TC == -1) {
    TC = +line;
  } else if (line.split(" ").length == 3) {
    [M, N, K] = line.split(" ").map((a) => +a);
    cabbCount = 0;
    cabbFarm = Array.from(Array(M), () => Array(N).fill(0));
  } else if (line.split(" ").length == 2) {
    const cabb = line.split(" ").map((a) => +a);
    cabbFarm[cabb[0]][cabb[1]] = 1;

    if (++cabbCount == K) {
      result[result.length] = algorila(cabbFarm);
      TCount++;
    }
  }

  if (TC == TCount) {
    rl.close();
  }
}).on("close", function () {
  console.log(result.join("\n"));

  process.exit();
});

function algorila(argFarm) {
  const farm = argFarm;
  const d = [
    [0, 1],
    [1, 0],
    [0, -1],
    [-1, 0],
  ];
  let warm = 0;
  function DFS(x, y) {
    farm[x][y] = 0;

    for (let i = 0; i < 4; i++) {
      const dx = x + d[i][0],
        dy = y + d[i][1];
      if (!(dx < 0 || dx >= M || dy < 0 || dy >= N || farm[dx][dy] != 1)) {
        DFS(dx, dy);
      }
    }
  }

  for (let i = 0; i < M; i++) {
    for (let j = 0; j < N; j++) {
      if (farm[i][j] == 1) {
        warm++;
        DFS(i, j);
      }
    }
  }

  return warm;
}

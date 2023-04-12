/* node.js */

// 1888 곰팡이

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  // input: process.stdin,
  input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐
/** @type {number} */
let inputNum = -1; // 입력 개수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = line.split(" ").map((a) => +a)[0];
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  let wallMap = input.map((line) => {
    return line.split("").map((a) => +a);
  });
  const [M, N] = [wallMap.length, wallMap[0].length];

  let timeSpent = 0;
  while (simulateMap() > 1) {
    timeSpent++;
  }

  console.log(timeSpent);

  /**
   * 현재 곰팡이 지도를 1단계 시뮬레이션한다. 함수 연산 결과로 곰팡이 지도가 다음 단계로 변경된다.
   * @returns 현재 곰팡이 지도의 곰팡이 군집 수
   */
  function simulateMap() {
    /**  시뮬레이션 결과 다음 단계를 기록할 지도 */
    const nextMap = JSON.parse(JSON.stringify(wallMap));

    /** 곰팡이 군집 수 */
    let moldsCnt = 0;

    // 지도 순회
    const queue = [];
    let head = 0;
    const dx = [
      [1, 0],
      [-1, 0],
      [0, 1],
      [0, -1],
    ];
    for (let i = 0; i < M; i++) {
      for (let j = 0; j < N; j++) {
        /** 곰팡이의 번식력 */
        const k = wallMap[i][j];
        if (k > 0) {
          // 곰팡이, BFS 큐에 좌표와 번식력 삽입
          queue.push([i, j, k]);
          wallMap[i][j] = 0;
          moldsCnt++;
        }

        // BFS
        while (queue.length - head) {
          const [X, Y, k] = queue[head++];

          for (let i = 0; i < 4; i++) {
            const [dX, dY] = [X + dx[i][0], Y + dx[i][1]];
            if (checkInvalidXY(dX, dY)) {
              continue;
            }

            const dK = wallMap[dX][dY];
            if (dK <= 0) continue; // 곰팡이가 아닌 or 이미 확인한 영역

            queue.push([dX, dY, dK]);
            wallMap[dX][dY] = 0; // 이미 지나간 영역 마킹
          }

          if (k > 0) {
            // 다음 지도에 곰팡이 번식 시뮬레이션
            for (let iX = X - k; iX <= X + k; iX++) {
              for (let iY = Y - k; iY <= Y + k; iY++) {
                if (checkInvalidXY(iX, iY)) continue;

                // 더 큰값으로 입력
                nextMap[iX][iY] = Math.max(k, nextMap[iX][iY]);
              }
            }
          }
        }
      }
    }

    wallMap = nextMap; // 현재 지도에 시뮬레이션 결과를 반영

    return moldsCnt;
  }

  /** 좌표 XY가 Map 범위 밖으로 벗어나는지 확인 */
  function checkInvalidXY(dX, dY) {
    return dX >= M || dX < 0 || dY >= N || dY < 0;
  }
}

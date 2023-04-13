/* node.js */

// 1119 그래프

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
let inputNum = -1; // 입력 개수

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
  const cityMap = input.map((l) => l.split(""));
  const N = cityMap.length;

  if (N === 1) {
    console.log(0);
    return;
  }

  /** 여유 도로 수 */
  let spareEdges = 0;
  /** 도시들이 도로로 연결되어 있는 생활권의 수 */
  let nOfArea = 0;

  /** 방문 정보 기록 배열 */
  const visit = Array(N).fill(false);

  // 인접행렬을 순회하며 DFS
  for (let curIndex = 0; curIndex < N; curIndex++) {
    if (visit[curIndex]) continue; // 이미 DFS를 수행한 도시
    nOfArea++; // 새로 방문한 도시일 경우 생활권 수 + 1

    /** DFS용 Stack */
    const stack = [];

    stack.push(curIndex);
    visit[curIndex] = true;

    let nOfVertex = 0;
    let nOfEdge = 0;

    // DFS 시작
    while (stack.length) {
      const i = stack.pop();
      nOfVertex++;

      // 도시<i>의 모든 도로 확인
      for (let j = 0; j < N; j++) {
        if (cityMap[i][j] === "Y") {
          // 도시<i>와 도시<j>가 도로로 연결되어 있음
          nOfEdge++;
          if (!visit[j]) {
            visit[j] = true; // 도시 방문 체크
            stack.push(j);
          }
        }
      }
    }

    // 여유 간선 수를 추가
    spareEdges += nOfEdge / 2 - (nOfVertex - 1);

    if (nOfVertex === 1) {
      // ???????????
      console.log(-1);
      return;
    }
  }

  // 사이클 수 만큼 도로를 내어줄 수 있음
  console.log(spareEdges >= nOfArea - 1 ? nOfArea - 1 : -1);
}

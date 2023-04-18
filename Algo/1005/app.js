/* node.js */

// 1005 ACM Craft

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

let T = -1,
  N = -2,
  K,
  D = [],
  W,
  edges = {},
  edgesCnt = 0,
  curTC = 0;

/** 출력 버퍼, 몰아서 출력하기 */
const outputBuffer = [];

rl.on("line", function (line) {
  if (T == -1) {
    // 테스트 케이스 수 입력
    T = +line;
    return;
  }

  if (N == -2) {
    // 테스트 케이스 별 입력 시작, 건물 수&간선 수 입력
    [N, K] = line.split(" ").map((a) => +a);
    return;
  }

  if (D.length == 0) {
    // 건물 소모 시간 입력
    D = line.split(" ").map((a) => +a);
    return;
  }

  if (edgesCnt < K) {
    // 간선 입력
    const [from, to] = line.split(" ").map((a) => +a - 1);

    edges[to] ??= [];
    edges[to].push(from);

    edgesCnt++;
    return;
  }

  // 테스트 케이스 입력 종료, 알고리즘 시작
  W = +line - 1;
  algorila();
  curTC++;
  N = -2;
  D = [];
  edges = {};
  edgesCnt = 0;

  if (curTC >= T) {
    // 테스트 종료 분기
    rl.close();
    return;
  }
}).on("close", function () {
  console.log(outputBuffer.join("\n"));
  process.exit();
});

function algorila() {
  /** 소요 시간을 메모하기 위한 배열 */
  const memo = Array(N).fill(undefined);

  function calcDelay(W) {
    if (memo[W] !== undefined) {
      // 미리 계산된 값 사용하기
      return memo[W];
    }

    /** W 건물의 소모시간 */
    const thisDelay = D[W];

    let slowest = 0;

    if (!edges[W]) {
      // 선행 건물 없음, 이 건물의 비용 반환하기.
    } else {
      // 가장 오래 걸리는 경로 선택
      slowest = Math.max(...edges[W].map((from) => calcDelay(from)));
    }
    memo[W] = thisDelay + slowest;
    return memo[W];
  }

  outputBuffer.push(calcDelay(W));
}

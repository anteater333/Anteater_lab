/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -3; // 입력 갯수

/** 사람 수 */
let N;
/** 문제 (P1과 P2 사이 촌수는?) */
let P1, P2;
/** 촌수 그래프 (Matrix) */
const familyMat = [];

rl.on("line", function (line) {
  if (inputNum == -3) {
    N = +line;
    for (let i = 0; i < N; i++) familyMat[i] = new Array(N).fill(0);
    inputNum++;
  } else if (inputNum == -2) {
    [P1, P2] = line.split(" ").map((a) => +a - 1);
    inputNum++;
  } else if (inputNum == -1) {
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
  input.forEach((relation) => {
    const [parent, child] = relation.split(" ").map((a) => +a - 1);
    familyMat[parent][child] = 1;
    familyMat[child][parent] = 1;
  });

  console.log(BFS(P1, P2));
}

function BFS(P1, P2) {
  const searchQ = [{ index: P1, cost: 0 }];
  let head = 0;
  let tail = 1;

  while (head < tail) {
    const cIndex = searchQ[head].index;
    const cCost = searchQ[head++].cost;

    if (cIndex == P2) {
      return cCost;
    }

    for (let i = 0; i < N; i++) {
      if (familyMat[cIndex][i]) {
        familyMat[cIndex][i] = 0;
        familyMat[i][cIndex] = 0;
        searchQ[tail++] = { index: i, cost: cCost + 1 };
      }
    }

    if (head == tail) {
      return -1;
    }
  }
}

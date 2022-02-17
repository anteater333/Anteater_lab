/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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

let N, start, dest;

const fibTreeCounts = [1, 1];
function fibTreeSize(N) {
  if (fibTreeCounts.length < N) {
    for (let i = fibTreeCounts.length; i <= N; i++) {
      fibTreeCounts[i] = fibTreeCounts[i - 1] + fibTreeCounts[i - 2] + 1;
    }
  }

  return fibTreeCounts[N];
}

/**
 *
 * @param {*} N 피이보나치 트리 서브셋 N
 * @param {*} nodeNumber 목표 위치
 * @param {*} root 현재 재귀 호출의 root. 초기값 1
 * @param {*} path 경로 저장 배열 (Reference)
 */
function getPathFromRoot(N, nodeNumber, root, path) {
  if (nodeNumber == root) {
    return;
  }
  const maxLeftPossibleNode = fibTreeSize(N - 2);
  const condition = nodeNumber <= root + maxLeftPossibleNode;
  if (condition) {
    path.push("L");
    getPathFromRoot(N - 2, nodeNumber, root + 1, path);
  } else {
    path.push("R");
    getPathFromRoot(N - 1, nodeNumber, root + maxLeftPossibleNode + 1, path);
  }
}

function pathCalc(start, dest) {
  let branch = -1;

  let path = "";

  for (let i = 0; i < start.length; i++) {
    if (start[i] != dest[i]) {
      // 분기 발생
      branch = i;
      break;
    }
  }

  if (branch < 0) {
    // 분기 발생 안함
    if (start.length >= dest.length) {
      // 분기 발생 안했는데 start까지 길이가 더 김
      // start 입장에선 길이 차이만큼 dest 까지 올라가야함
      path += new Array(start.length - dest.length).fill("U").join("");
    } else {
      // 분기 발생 안했는데 dest까지 길이가 더 김
      // start 입장에선 길이 차이만큼 dest 까지 내려가야함.
      path += dest.slice(start.length).join("");
    }
  } else {
    // 분기 발생 함
    // 시작 점에선 분기점까지 올라갸아함
    for (let i = branch; i < start.length; i++) {
      path += "U";
    }
    // 분기점 이후로 목적지까지 내려가야함
    for (let i = branch; i < dest.length; i++) {
      path += dest[i];
    }
  }

  return path;
}

function algorila() {
  [N, start, dest] = input[0].split(" ").map((a) => +a);

  const size = fibTreeSize(N);
  const pathToStart = [];
  getPathFromRoot(N, start, 1, pathToStart);
  const pathToDest = [];
  getPathFromRoot(N, dest, 1, pathToDest);
  //   console.log(pathToStart, pathToDest);

  console.log(pathCalc(pathToStart, pathToDest));

  //   console.log(process.memoryUsage().heapUsed / 1024 / 1024);
}

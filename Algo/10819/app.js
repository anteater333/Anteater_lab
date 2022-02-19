/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

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

let N, arr;
let max = 0;
function algorila() {
  N = +input[0];
  arr = input[1].split(" ").map((a) => +a);

  const diffArr = new Array(N);
  for (let i = 0; i < N; i++) {
    diffArr[i] = [];
    for (let j = 0; j < N; j++) {
      if (i == j) diffArr[i][j] = -1;
      else diffArr[i][j] = Math.abs(arr[i] - arr[j]);
    }
  }

  trackFramework(diffArr);

  console.log(max);
  console.log(maxPath.map((i) => arr[i]));
}

function trackFramework(map) {
  for (let i = 0; i < N; i++) {
    tracking(JSON.parse(JSON.stringify(map)), i, 0, [i]);
  }
}

let maxPath = [];
function tracking(map, rowIdx, sum, path) {
  //   console.log(sum, path);
  if (path.length == N) {
    // console.log(sum, path);
    max = max < sum ? sum : max;
    maxPath = max < sum ? maxPath : path;
  } else {
    for (let i = 0; i < N; i++) {
      if (path.findIndex((el) => el == i) != -1) continue;
      else {
        tracking(map, i, sum + map[rowIdx][i], [...path, i]);
      }
    }
  }
}

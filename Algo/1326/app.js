/* node.js */

// 1326 폴짝폴짝

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
let inputNum = 3; // 입력 갯수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  // 입력 정제
  const [NStr, stonesStr, dest] = input;
  const N = +NStr;

  const stones = stonesStr.split(" ").map((a) => +a);
  const [a, b] = dest.split(" ").map((a) => +a - 1); // 목적지 a => b, Array index 기반

  // a to b 경로 그래프 탐색
  const queue = [[a, 0]];
  let head = 0;
  const visited = Array(N).fill(false);
  visited[a] = true;

  let distance = -1;
  do {
    const [i, curDistance] = queue[head++];

    if (i === b) {
      distance = curDistance;
      break;
    }

    if (stones[i] === 0) continue;

    for (let j = 1; i + stones[i] * j < N; j++) {
      if (!visited[i + stones[i] * j]) {
        queue.push([i + stones[i] * j, curDistance + 1]);
        visited[i + stones[i] * j] = true;
      }
    }
    for (let j = 1; i - stones[i] * j >= 0; j++) {
      if (!visited[i - stones[i] * j]) {
        queue.push([i - stones[i] * j, curDistance + 1]);
        visited[i - stones[i] * j] = true;
      }
    }
  } while (queue.length - head);

  console.log(distance);
}

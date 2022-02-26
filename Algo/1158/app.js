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

function algorila() {
  const [N, K] = input[0].split(" ").map((a) => +a);

  const arr = Array.from({ length: N }, (_, i) => i + 1);

  const result = [];

  let kCnt = 0;
  let idx = 0;
  let catchCnt = 0;
  while (catchCnt < N) {
    idx = idx % N;
    if (arr[idx] == -1) {
      idx++;
      continue;
    }
    kCnt++;
    if (kCnt == K) {
      result.push(arr[idx]);
      arr[idx] = -1;
      catchCnt++;
      kCnt = 0;
    }
    idx++;
  }

  process.stdout.write("<");
  for (let i = 0; i < result.length - 1; i++) {
    process.stdout.write(String(result[i]));
    process.stdout.write(", ");
  }
  console.log(result[result.length - 1] + ">");
}

/* node.js */

/**
 * 2022-08-02
 * 이 문제에 대한 코드는 Github Copilot을 켜놓은 것을 까먹은 채로 풀었다가
 * 갑자기 지 혼자 코드가 나오길래 신기해서 그대로 적용시켜본 것입니다.
 * #0, #1, #2로 구분된 영역이 Copilot이 작성한 코드입니다.
 */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수
let N, M;

rl.on("line", function (line) {
  if (inputNum == -1) {
    [N, M] = line.split(" ").map((a) => +a);
    inputNum = N;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  // #0
  const arr = Array(100)
    .fill(0)
    .map(() => Array(100).fill(0));

  // #1
  input.forEach((box) => {
    const [x1, y1, x2, y2] = box.split(" ").map((a) => +a);
    for (let i = x1; i <= x2; i++) {
      for (let j = y1; j <= y2; j++) {
        arr[i][j] += 1;
      }
    }
  });

  // #2
  let result = 0;
  for (let i = 0; i < 100; i++) {
    for (let j = 0; j < 100; j++) {
      if (arr[i][j] > M) result++;
    }
  }

  console.log(result);
}

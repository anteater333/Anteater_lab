/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = line;
  } else {
    input.push(line.split(" ").map((a) => parseInt(a)));
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const sortedInput = input.sort((A, B) => {
    return A[1] - B[1] === 0 ? A[0] - B[0] : A[1] - B[1];
  });

  let count = 1;
  let done = false;
  let i = 0;
  while (!done) {
    let endTime = sortedInput[i][1];

    done = true;
    for (let j = i + 1; j < sortedInput.length; j++) {
      let startTime = sortedInput[j][0];
      if (startTime < endTime) {
        // 이전 회의의 종료 시각보다 시작 시각이 빠르므로 회의 불가
        continue;
      } else {
        // 가장 빨리 끝나는 회의를 선택
        count++;
        i = j;
        done = false;
        break;
      }
    }
  }

  console.log(count);
}

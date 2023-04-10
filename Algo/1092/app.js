/* node.js */

// 1092 배

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
let inputNum = 4; // 입력 개수

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
  const [towers, boxes] = [
    input[1]
      .split(" ")
      .map((a) => +a)
      .sort((a, b) => b - a),
    input[3]
      .split(" ")
      .map((a) => +a)
      .sort((a, b) => a - b),
  ];

  if (towers[0] < boxes[boxes.length - 1]) {
    // 문제 해결 불가
    // 가장 무거운 화물이 가장 강한 크레인보다 무거울 때
    console.log(-1);
    return;
  }

  let uselessCnt = 0;
  let movedCnt = 0;
  /** time spent */
  let min = 0;
  while (movedCnt < boxes.length) {
    if (uselessCnt === towers.length) {
      // 문제 해결 불가
      // 아직 옮겨야 할 화물은 남아있으나 모든 크레인이 사용 불가 상태일 때
      min = -1;
      break;
    }

    min++; // 1분 동안 가능한 크레인 모두 가동
    for (let i = 0; i < towers.length; i++) {
      // 강한 크레인부터 순회

      if (towers[i] === -1) continue; // 더이상 옮길 수 있는게 없는 크레인

      // 이 크레인이 옮길 수 있는 가장 무거운 화물 탐색
      let boxIdx = -1;
      for (let j = boxes.length - 1; j >= 0; j--) {
        if (boxes[j] === -1) continue; // 이미 옮김
        if (boxes[j] <= towers[i]) {
          boxIdx = j;
          break;
        }
      }

      if (boxIdx >= 0) {
        // 화물 옮김 처리
        boxes[boxIdx] = -1;
        movedCnt++;
      } else {
        // 옮길 수 있는 화물이 없음. 크레인 사용 불가 처리.
        towers[i] = -1;
        uselessCnt++;
      }
    }
  }

  console.log(min);
}

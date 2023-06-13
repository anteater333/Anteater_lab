/* node.js */

// 1461 도서관

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
let inputNum = 2; // 입력 개수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const [N, M] = input[0].split(" ").map((a) => +a);
  const left = [],
    right = [];

  // 책 위치 좌/우 분류
  input[1].split(" ").forEach((book) => {
    if (+book < 0) left.push(+book * -1); // 절댓값으로 변환
    else right.push(+book);
  });

  // 각 배열별로 책 위치 가까운 순으로 정렬
  left.sort((a, b) => a - b);
  right.sort((a, b) => a - b);

  // 움직임 비용 배열
  const moves = [];
  while (left.length > 0) {
    const move = [];
    for (let i = 0; i < M; i++) {
      if (left.length === 0) break;
      move.push(left.pop());
    }
    moves.push(move[0]); // 가장 먼 수치만 기록
  }
  while (right.length > 0) {
    const move = [];
    for (let i = 0; i < M; i++) {
      if (right.length === 0) break;
      move.push(right.pop());
    }
    moves.push(move[0]); // 가장 먼 수치만 기록
  }

  // 결과값 계산
  moves.sort((a, b) => a - b); // NOTE: 비용 배열을 굳이 정렬하지 않아도 되는 방법이 있을 듯
  const last = moves.pop();
  const total = moves.reduce((pv, cv) => pv + cv * 2, 0) + last;

  console.log(total);
}

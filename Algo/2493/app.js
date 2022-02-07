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

function algorila() {
  const N = parseInt(input[0]);
  const towers = input[1].split(" ").map((a) => +a);

  let tallestI = 0;
  let validTowers = [0];
  const answer = [0];

  for (let i = 1; i < N; i++) {
    if (towers[tallestI] <= towers[i]) {
      tallestI = i; // 새로운 마천루의 갱신
      validTowers = [i]; // 앞서있던 tower들 무시
      answer[answer.length] = 0; // 이전에 더 큰 타워가 없었으므로 수신 불가
      continue;
    }

    while (towers[validTowers[validTowers.length - 1]] <= towers[i]) {
      // 현재 타워보다 큰 타워를 만날 때 까지 pop할거임.
      validTowers.pop();
    }
    answer[answer.length] = validTowers[validTowers.length - 1] + 1;
    validTowers[validTowers.length] = i;
  }

  console.log(answer.join(" "));
}

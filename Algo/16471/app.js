/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 3; // 입력 갯수

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
  const player = input[1]
    .split(" ")
    .map((a) => +a)
    .sort((a, b) => a - b);
  const enemy = input[2]
    .split(" ")
    .map((a) => +a)
    .sort((a, b) => a - b);

  let point = 0;

  for (let i = 0, cursor = 0; i < N; i++) {
    const cardP = player[i];
    for (; cursor < N; cursor++) {
      const cardE = enemy[cursor];
      if (cardP < cardE) {
        point++;
        cursor++;
        // console.log(`Player ${cardP} vs Enemy ${cardE}`);
        break;
      }
    }

    if (cursor >= N) {
      // 더이상 사장이 접대할 카드가 없다.
      break;
    }
  }

  //   console.log(point);

  if (point >= (N + 1) / 2) {
    console.log("YES");
  } else {
    console.log("NO");
  }
}

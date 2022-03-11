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
  const N = +input[0];

  let cnt = 0;
  for (let i = 1; i <= N; i++) {
    cnt += hans(i) ? 1 : 0;
  }

  console.log(cnt);
}

function hans(argNumber) {
  // 단단한 코딩
  if (argNumber / 100 < 1) return true;
  let number = argNumber;
  const numbers = [number % 10];
  number = parseInt(number / 10);
  numbers[1] = number % 10;
  number = parseInt(number / 10);

  let diff = numbers[0] - numbers[1];
  let idx = 2;
  while (number) {
    numbers[idx] = number % 10;
    if (numbers[idx - 1] - numbers[idx] != diff) return false;
    number = parseInt(number / 10);
    idx++;
  }

  return true;
}

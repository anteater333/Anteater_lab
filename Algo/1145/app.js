/* node.js */

// 1145 적어도 대부분의 배수

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
  const members = input[0].split(" ").map((a) => +a);

  let number = 1;
  while (true) {
    let dCount = 0;

    for (let i = 0; i < 5 && dCount < 3; i++) {
      if (number % members[i] == 0) {
        dCount++;
      }
    }

    if (dCount >= 3) {
      console.log(number);
      break;
    }

    number++;
  }
}

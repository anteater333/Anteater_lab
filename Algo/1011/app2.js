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
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const resultBuffer = [];
  input.forEach((TC) => {
    resultBuffer[resultBuffer.length] = predict(
      ...TC.split(" ").map((a) => +a)
    );
  });

  console.log(resultBuffer.join("\n"));
}

function predict(x, y) {
  const distance = y - x;
  const sqrted = Math.sqrt(distance);

  if (sqrted - Math.floor(sqrted) < 0.5) {
    // SQRT가 .5 아래일 경우
    return Math.floor(sqrted) + Math.ceil(sqrted) - 1;
  } else {
    // SQRT가 .5 위일 경우
    return 2 * Math.ceil(sqrted) - 1;
  }
}

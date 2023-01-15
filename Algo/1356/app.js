/* node.js */

// 1356 유진수

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
  const numStr = input[0];

  let isNewjeans = false;

  for (let index = 0; index < input[0].length - 1; index++) {
    if (
      numStr
        .slice(0, index + 1)
        .split("")
        .map((a) => +a)
        .reduce((pv, cv) => {
          return pv * cv;
        }, 1) ===
      numStr
        .slice(index + 1, numStr.length)
        .split("")
        .map((a) => +a)
        .reduce((pv, cv) => {
          return pv * cv;
        }, 1)
    ) {
      isNewjeans = true;
      break;
    }
  }

  console.log(isNewjeans ? "YES" : "NO");
}

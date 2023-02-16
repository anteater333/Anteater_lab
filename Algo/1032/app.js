/* node.js */

// 1032 명령 프롬프트

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
  const result = input[0].split("");

  for (let i = 1; i < input.length; i++) {
    for (let j = 0; j < result.length; j++) {
      if (result[j] !== "?" && input[i][j] !== result[j]) {
        result[j] = "?";
      }
    }
  }

  console.log(result.join(""));
}

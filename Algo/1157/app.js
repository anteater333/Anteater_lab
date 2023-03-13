/* node.js */

// 1157 단어 공부

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
  const charDict = {};

  const cntChar = (char) => {
    if (char in charDict) {
      charDict[char]++;
      return;
    }

    charDict[char] = 1;
  };

  const word = input[0].toUpperCase();

  word.split("").forEach((char) => {
    cntChar(char);
  });

  let maxCnt = 0;
  let maxChar = "?";
  for (const char in charDict) {
    const cnt = charDict[char];

    if (cnt === maxCnt) {
      maxChar = "?";
      continue;
    }

    if (cnt > maxCnt) {
      maxCnt = cnt;
      maxChar = char;
    }
  }

  console.log(maxChar);
}

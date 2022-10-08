/* node.js */

// 2391 Sascha

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let dict = []; // 입력 큐
let inputCountLen = -1; // 입력 갯수
let inputCount = 0;
let subCount = -1;
let subCountLen = 0;

let sascha = "";

rl.on("line", function (line) {
  if (inputCountLen == -1) {
    inputCountLen = +line;
  } else {
    if (subCount < 0) {
      sascha = line;
    } else if (subCount == 0) {
      subCountLen = +line;
    } else {
      dict.push(line);

      if (dict.length == subCountLen) {
        algorila();
        subCount = -2;
        inputCount++;
        dict = [];
      }
    }
    subCount++;
    if (inputCount == inputCountLen) rl.close();
  }
}).on("close", function () {
  process.exit();
});

function algorila() {
  let minReplaceCnt = 130;
  let mostLikelyWord = "";

  dict.forEach((word) => {
    let saschaWord = sascha;

    let replaceCnt = 0;

    for (let i = 0; i < saschaWord.length; i++) {
      if (word[i] != saschaWord[i]) {
        replaceCnt++;
      }
    }

    if (minReplaceCnt > replaceCnt) {
      minReplaceCnt = replaceCnt;
      mostLikelyWord = word;
    }
  });

  console.log(mostLikelyWord);
}

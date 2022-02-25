/* node.js */
/** 시간초과가 나는 이유 이해하기 */

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
  const text = input[0].split("");
  const pattern = input[1].split("");
  const tLen = text.length;
  const pLen = pattern.length;

  const pos = [];

  const kmp = [0];
  let kmpCnt = 0;
  let j = 0;
  for (let i = 1; i < pLen; i++) {
    if (pattern[j] != pattern[i]) {
      kmpCnt = 0;
      j = 0;
    }
    if (pattern[j] == pattern[i]) {
      kmpCnt++;
      j++;
    }
    kmp[i] = kmpCnt;
  }

  // console.log(kmp);

  let i = 0;
  j = 0;
  while (i < tLen) {
    // console.log(i, i + j, text[i + j], pattern[j], j, kmp[j]);
    if (text[i + j] != pattern[j]) {
      // 패턴 불일치
      i = i + (j + 1) - (j - 1 >= 0 ? kmp[j - 1] + 1 : 0);
      j = j - 1 >= 0 ? kmp[j - 1] : 0;
    } else if (text[i + j] == pattern[j]) {
      // 문자 일치
      if (j == pLen - 1) {
        // 패턴 일치
        pos[pos.length] = i + 1;
        i = i + pLen - kmp[j - 1] - 1;
        j = 0;
      } else {
        j++;
      }
    }
  }

  console.log(pos.length);
  console.log(pos.join(" "));
}

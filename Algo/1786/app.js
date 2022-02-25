/* node.js */

const input = require("fs").readFileSync("input.txt").toString().split("\n");

algorila();

// const rl = require("readline").createInterface({
//   input: process.stdin,
//   output: process.stdout,
// });

// let input = []; // 입력 큐
// let inputNum = 2; // 입력 갯수

// rl.on("line", function (line) {
//   if (inputNum == -1) {
//     inputNum = line;
//   } else {
//     input.push(line);
//     if (input.length == inputNum) rl.close();
//   }
// }).on("close", function () {
//   algorila();

//   process.exit();
// });

function algorila() {
  const text = input[0]; //.split("");
  const pattern = input[1]; //.split("");
  const tLen = text.length;
  const pLen = pattern.length;

  const pos = [];

  let i = -1,
    j = 0;
  const kmp = [i];

  while (j < pLen) {
    if (i == -1 || pattern[i] == pattern[j]) kmp[++j] = ++i;
    else i = kmp[i];
  }

  // console.log(kmp);

  (i = 0), (j = 0);
  while (i < tLen) {
    if (j == -1 || text[i] == pattern[j]) i++, j++;
    else j = kmp[j];

    if (j == pLen) {
      pos.push(i - pLen + 1);
      j = kmp[j];
    }
  }

  console.log(pos.length);
  console.log(pos.join(" "));
}

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
  console.log(
    Array.from(new Set(input))
      .sort((strA, strB) => {
        if (strA.length == strB.length) {
          if (strA < strB) return -1;
          if (strA == strB) return 0;
          if (strA > strB) return 1;
        } else {
          return strA.length - strB.length;
        }
      })
      .join("\n")
  );
}

/* node.js */

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
  const N = parseInt(input[0]);
  const A = input[1].split(" ").map((a) => parseInt(a));

  let result = "-1";

  const NGEStack = [A[N - 1]];
  for (let i = N - 2; i >= 0; i--) {
    let NGE = -1;
    while (NGEStack.length) {
      if (A[i] >= NGEStack[NGEStack.length - 1]) {
        NGEStack.pop();
      } else {
        NGE = NGEStack[NGEStack.length - 1];
        break;
      }
    }
    NGEStack[NGEStack.length] = A[i];
    result = NGE + " " + result;
  }
  console.log(result);
}

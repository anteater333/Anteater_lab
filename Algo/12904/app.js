/* node.js */

// 12904 A와 B

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

/** @type {Array<string>} */
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

function reverse(array, endLen) {
  for (let i = 0, j = endLen - 1; i < (endLen - 1) / 2; i++, j--) {
    let tmp = array[i];
    array[i] = array[j];
    array[j] = tmp;
  }
}

function algorila() {
  /** @type {Array<string>} */
  const S = input[0];
  /** @type {Array<string>} */
  const T = input[1].split("").map((a) => a);

  for (let i = T.length - 1; i >= S.length; i--) {
    if (T[i] === "B") {
      reverse(T, i);
    } else {
      // Do nothing
    }
  }

  result = T.join("").slice(0, S.length);

  console.log(T.join("").slice(0, S.length) === S ? 1 : 0);
}

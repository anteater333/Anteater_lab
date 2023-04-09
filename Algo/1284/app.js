/* node.js */

// 1284 집 주소

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐

rl.on("line", function (line) {
  if (line == 0) {
    rl.close();
  } else {
    input.push(line);
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  function getWidth(number) {
    return (
      number
        .toString()
        .split("")
        .map((a) => +a)
        .reduce((sum, digit) => {
          return sum + (digit === 0 ? 5 : digit === 1 ? 3 : 4);
        }, 0) + 1
    );
  }

  input.forEach((line) => {
    console.log(getWidth(line));
  });
}

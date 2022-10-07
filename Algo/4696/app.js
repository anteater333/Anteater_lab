/* node.js */

// 4696 St. Ives

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐

rl.on("line", function (line) {
  if (line == "0") {
    rl.close();
  } else {
    input.push(line);
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  input.forEach((line) => {
    const factor = +line;

    const result = 1 + factor + factor ** 2 + factor ** 3 + factor ** 4;

    console.log(result.toFixed(2));
  });
}

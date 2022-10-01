/* node.js */

// 4714 Lunacy

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐

rl.on("line", function (line) {
  if (+line < 0) rl.close();
  else input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  input.forEach((w) => {
    console.log(
      `Objects weighing ${(+w).toFixed(2)} on Earth will weigh ${(
        +w * 0.167
      ).toFixed(2)} on the moon.`
    );
  });
}

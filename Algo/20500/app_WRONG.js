/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
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
  const N = parseInt(input[0]);

  let Ezreal = [1n, 5n];

  for (let i = 1; i < N; i++) {
    let newZreal = [];
    const factor1 = BigInt(1n * 10n ** BigInt(i));
    const factor5 = BigInt(5n * 10n ** BigInt(i));
    Ezreal.forEach((value) => {
      const value1 = value + factor1;
      const value5 = value + factor5;
      if (value1 % 100n == 15n || value1 % 100n == 55n) {
        newZreal[newZreal.length] = value1;
      }
      if (value5 % 100n == 15n || value5 % 100n == 55n) {
        newZreal[newZreal.length] = value5;
      }
    });
    Ezreal = newZreal;
  }

  let count = 0;
  const answer = [];
  const quot = [];
  Ezreal.forEach((value) => {
    if (value % 15n == 0) {
      answer[answer.length] = value;
      quot[quot.length] = value / 15n;
      count++;
    }
  });

  console.log(count, answer);
}

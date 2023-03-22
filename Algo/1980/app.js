/* node.js */

// 1980 햄버거 사랑

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐
/** @type {number} */
let inputNum = 1; // 입력 갯수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const [n, m, t] = input[0].split(" ").map((a) => +a);

  const maxNBurger = Math.floor(t / n);
  const maxMBurger = Math.floor(t / m);

  let nBurger,
    mBurger,
    totalBurger = 0,
    cokeTime = t;

  for (nBurger = 0; nBurger <= maxNBurger; nBurger++) {
    for (mBurger = 0; mBurger <= maxMBurger; mBurger++) {
      const curCoke = t - (nBurger * n + mBurger * m);
      if (curCoke < 0) break;

      const curBurger = nBurger + mBurger;
      if (cokeTime > curCoke) {
        totalBurger = curBurger;
        cokeTime = curCoke;
      } else if (cokeTime === curCoke && totalBurger < curBurger) {
        totalBurger = curBurger;
        cokeTime = curCoke;
      }
    }
  }

  console.log(`${totalBurger} ${cokeTime}`);
}

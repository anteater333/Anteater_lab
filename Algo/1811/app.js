/* node.js */

// 1811 Adjacent Mastermind

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (line == "#") rl.close();
  input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  input.forEach((TC) => {
    const [target, guess] = TC.split(" ").map((s) => s.split(""));
    const [_, guessOrigin] = TC.split(" ");

    // const ans = Array.from({ length: target.length });
    let [nBlack, nGrey, nWhite] = [0, 0, 0];

    // #1 find black
    for (let i = 0; i < target.length; i++) {
      if (target[i] === guess[i]) {
        // ans[i] = "B";
        nBlack++;
        target[i] = "-";
        guess[i] = "_";
      }
    }

    // #2 find grey
    for (let i = 0; i < target.length; i++) {
      if (i != 0 && target[i] === guess[i - 1]) {
        // ans[i] = "G";
        nGrey++;
        target[i] = "-";
        guess[i - 1] = "_";
      } else if (i != target.length - 1 && target[i] === guess[i + 1]) {
        // ans[i] = "G";
        nGrey++;
        target[i] = "-";
        guess[i + 1] = "_";
      }
    }

    // #3 find white
    for (let i = 0; i < target.length; i++) {
      const idx = guess.findIndex((el) => el === target[i]);
      if (idx != -1) {
        // ans[i] = "W";
        nWhite++;
        target[i] = "-";
        guess[idx] = "_";
      }
    }

    console.log(
      `${guessOrigin}: ${nBlack} black, ${nGrey} grey, ${nWhite} white`
    );
  });
}

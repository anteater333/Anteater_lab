/* node.js */

// 5220 Error Detection

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
  input.forEach((value) => {
    const [number, parity] = value.split(" ").map((a) => +a);
    const bit = number.toString(2);

    let odd = false;

    bit.split("").map((b) => {
      if (b === "1") {
        odd = !odd;
      }
    });

    if (odd == parity) console.log("Valid");
    else console.log("Corrupt");
  });
}

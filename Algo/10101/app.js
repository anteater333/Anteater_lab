/* node.js */

// 10101 삼각형 외우기

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
let inputNum = 3; // 입력 개수

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
  const angles = input.map((a) => +a);

  if (angles[0] + angles[1] + angles[2] !== 180) {
    console.log("Error");
  } else if (angles[0] === angles[1] && angles[1] === angles[2]) {
    console.log("Equilateral");
  } else if (
    angles[0] === angles[1] ||
    angles[1] === angles[2] ||
    angles[0] === angles[2]
  ) {
    console.log("Isosceles");
  } else {
    console.log("Scalene");
  }
}

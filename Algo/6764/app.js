/* node.js */

// 6764 Sounds fishy!

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 4; // 입력 갯수

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
  const depths = input.map((a) => +a);

  let flag = false;
  let status = 0; // 0 : identical, 1 : increasing, 2 : decreasing, 3 : otherwise

  const distances = [];
  for (let i = 1; i < 4; i++) {
    distances[i - 1] = depths[i - 1] - depths[i];
  }

  if (distances.every((d) => d < 0)) {
    // increasing
    console.log("Fish Rising");
  } else if (distances.every((d) => d > 0)) {
    // dereasing
    console.log("Fish Diving");
  } else if (distances.every((d) => d === 0)) {
    // identical
    console.log("Fish At Constant Depth");
  } else {
    // otherwise
    console.log("No Fish");
  }
}

/* node.js */

// 5717 상근이의 친구들

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
  if (line === "0 0") {
    rl.close();
    return;
  }
  console.log(
    line
      .split(" ")
      .map((a) => +a)
      .reduce((pv, cv) => pv + cv, 0)
  );
}).on("close", function () {
  process.exit();
});

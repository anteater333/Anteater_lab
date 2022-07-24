/* node.js */

// 3432 Game

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
    if (input.length == inputNum * 5) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const Z = +inputNum;

  const indexSet = [];
  const outputBuffer = [];

  indexSet.push([
    [2, 0],
    [3, 1],
    [4, 2],
  ]);
  indexSet.push([
    [1, 0],
    [2, 1],
    [3, 2],
    [4, 3],
  ]);
  indexSet.push([
    [0, 0],
    [1, 1],
    [2, 2],
    [3, 3],
    [4, 4],
  ]);
  indexSet.push([
    [0, 1],
    [1, 2],
    [2, 3],
    [3, 4],
  ]);
  indexSet.push([
    [0, 2],
    [1, 3],
    [2, 4],
  ]);

  indexSet.push([
    [0, 2],
    [1, 1],
    [2, 0],
  ]);
  indexSet.push([
    [0, 3],
    [1, 2],
    [2, 1],
    [3, 0],
  ]);
  indexSet.push([
    [0, 4],
    [1, 3],
    [2, 2],
    [3, 1],
    [4, 0],
  ]);
  indexSet.push([
    [1, 4],
    [2, 3],
    [3, 2],
    [4, 1],
  ]);
  indexSet.push([
    [2, 4],
    [3, 3],
    [4, 2],
  ]);
  for (let j = 0; j < 5; j++) {
    let builder = [];
    for (let k = 0; k < 5; k++) {
      builder.push([j, k]);
    }
    indexSet.push(builder);
  }
  for (let j = 0; j < 5; j++) {
    let builder = [];
    for (let k = 0; k < 5; k++) {
      builder.push([k, j]);
    }
    indexSet.push(builder);
  }

  for (let i = 0; i < Z * 5; i += 5) {
    const board = input.slice(i, i + 5).map((line) => line.split(""));

    let isA = false,
      isB = false;

    for (let j = 0; j < indexSet.length; j++) {
      const text = indexSet[j].map((idx) => board[idx[0]][idx[1]]).join("");
      if (!isA && text.includes("AAA")) {
        isA = true;
      }
      if (!isB && text.includes("BBB")) {
        isB = true;
      }

      if (isA && isB) break;
    }

    outputBuffer.push(isA === isB ? "draw" : isA ? "A wins" : "B wins");
  }

  console.log(outputBuffer.join("\n"));
}

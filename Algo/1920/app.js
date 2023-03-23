/* node.js */

// 1920 수 찾기

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
let inputNum = 4; // 입력 갯수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

class BinaryTree {
  /** @type Array<number> */
  tree;
  constructor(_tree) {
    this.tree = _tree;
  }

  binarySearch(target) {
    const tree = this.tree;
    let index = Math.floor(tree.length / 2);
    let min = 0,
      max = tree.length - 1;
    while (true) {
      if (tree[index] === target) {
        return true;
      }

      if (tree[index] < target) {
        if (index === max) return false;
        min = index + 1;
      } else if (tree[index] > target) {
        if (index === min) return false;
        max = index - 1;
      }
      index = min + Math.floor((max + 1 - min) / 2);
    }
  }
}

function algorila() {
  const A = input[1]
      .split(" ")
      .map((a) => +a)
      .sort((a, b) => a - b),
    Q = input[3].split(" ").map((a) => +a);

  const B = new BinaryTree(A);

  const output = [];
  Q.forEach((target) => {
    const found = B.binarySearch(target);
    // if (found) console.log(target);
    output.push(found ? 1 : 0);
  });

  console.log(output.join("\n"));
}

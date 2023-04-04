/* node.js */

// 1629 곱셈

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

/**
 * a ** n = [(a ** n/2)(a ** n/2) when n is even, (a ** n/2)(a ** n/2)a when n is odd]
 * @param {*} a
 * @param {*} n
 * @returns
 */
function pow(a, n, C) {
  let res = 1n;
  while (n) {
    if (n & 1n) res = (res * a) % C; // n이 홀수
    a = (a * a) % C;
    n = n >> 1n; // n//2
  }

  return res % C;
}

function algorila() {
  const [A, B, C] = input[0].split(" ").map((a) => BigInt(a));

  console.log(pow(A, B, C).toString());
}

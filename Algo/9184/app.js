/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

const wMemo = Array.from({ length: 21 }, () => {
  return Array.from({ length: 21 }, () => {
    return Array.from({ length: 21 });
  });
});

rl.on("line", function (line) {
  if (line.split(" ").every((a) => a == "-1")) rl.close();
  else {
    const [a, b, c] = line.split(" ").map((a) => +a);
    const wVal = w(a, b, c);
    console.log(`w(${a}, ${b}, ${c}) = ${wVal}`);
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function w(a, b, c) {
  if (a <= 0 || b <= 0 || c <= 0) {
    return 1;
  }
  if (a > 20 || b > 20 || c > 20) {
    return w(20, 20, 20);
  }
  if (wMemo[a][b][c]) return wMemo[a][b][c];
  let rVal;
  if (a < b && b < c) {
    rVal = w(a, b, c - 1) + w(a, b - 1, c - 1) - w(a, b - 1, c);
    wMemo[a][b][c] = rVal;
    return rVal;
  }
  rVal =
    w(a - 1, b, c) +
    w(a - 1, b - 1, c) +
    w(a - 1, b, c - 1) -
    w(a - 1, b - 1, c - 1);
  wMemo[a][b][c] = rVal;
  return rVal;
}

function algorila() {}

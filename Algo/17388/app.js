/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

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
  const [S, K, H] = input[0].split(" ").map((a) => +a);

  if (S + K + H >= 100) {
    console.log("OK");
    return;
  }

  const badGuys = Math.min(S, K, H);
  if (badGuys == S) {
    console.log("Soongsil");
  } else if (badGuys == K) {
    console.log("Korea");
  } else {
    console.log("Hanyang");
  }
}

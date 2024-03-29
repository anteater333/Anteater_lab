/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

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
  const [L, P] = input[0].split(" ").map((a) => +a);
  const total = L * P;

  const distances = [];
  input[1]
    .split(" ")
    .map((a) => +a)
    .forEach((newspaper) => {
      distances.push(newspaper - total);
    });

  console.log(distances.join(" "));
}

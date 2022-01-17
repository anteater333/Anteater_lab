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
  const people = input[1]
    .split(" ")
    .map((a) => parseInt(a))
    .sort((a, b) => a - b);

  let waiting = people[0];
  for (let index = 1; index < parseInt(input[0]); index++) {
    people[index] += people[index - 1];
    waiting += people[index];
  }

  console.log(waiting);
}

/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = parseInt(line.split(" ")[0]);
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

const myQ = [];
let head = 0,
  tail = 0;

function algorila() {
  for (let i = 0; i < inputNum; i++) {
    cmd = input[i];
    if (cmd.includes("push")) {
      myQ[tail] = cmd.split(" ")[1];
      tail++;
    } else if (cmd.includes("pop")) {
      if (head === tail) {
        console.log(-1);
      } else {
        console.log(myQ[head]);
        head++;
      }
    } else if (cmd.includes("size")) {
      console.log(tail - head);
    } else if (cmd.includes("empty")) {
      console.log(head === tail ? 1 : 0);
    } else if (cmd.includes("front")) {
      console.log(head === tail ? -1 : myQ[head]);
    } else if (cmd.includes("back")) {
      console.log(head === tail ? -1 : myQ[tail - 1]);
    }
  }
}

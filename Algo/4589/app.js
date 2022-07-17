/* node.js */

// 4589 Gnome Sequencing

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
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  console.log("Gnomes:"); // 노움놈들

  input.forEach((TC) => {
    const gnomes = TC.split(" ").map((a) => +a);

    if (Math.abs(gnomes[0] - gnomes[1]) >= Math.abs(gnomes[0] - gnomes[2])) {
      console.log("Unordered");
    } else {
      console.log("Ordered");
    }
  });
}

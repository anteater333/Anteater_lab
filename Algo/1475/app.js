/* node.js */

// 1475 방 번호

const rl = require("readline").createInterface({
  input: process.stdin,
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

function algorila() {
  const roomNumber = input[0].split("");

  const numDict = {
    0: 0,
    1: 0,
    2: 0,
    3: 0,
    4: 0,
    5: 0,
    6: 0,
    7: 0,
    8: 0,
  };

  roomNumber.forEach((number) => {
    numDict[number === "9" ? "6" : number]++;
  });

  numDict[6] = Math.ceil(numDict[6] / 2);
  console.log(Math.max(...Object.values(numDict)));
}

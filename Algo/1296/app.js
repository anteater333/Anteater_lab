/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -2; // 입력 갯수

let yondu = "";

rl.on("line", function (line) {
  if (inputNum == -2) {
    yondu = line;
    inputNum++;
  } else if (inputNum == -1) {
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
  const yonduName = yondu.split("");
  const yonduL = getVar(yonduName, "L");
  const yonduO = getVar(yonduName, "O");
  const yonduV = getVar(yonduName, "V");
  const yonduE = getVar(yonduName, "E");
  const teams = input.sort();

  let maxRate = -1;
  let maxIdx = 0;
  teams.forEach((team, index) => {
    const teamName = team.split("");
    const L = getVar(teamName, "L") + yonduL;
    const O = getVar(teamName, "O") + yonduO;
    const V = getVar(teamName, "V") + yonduV;
    const E = getVar(teamName, "E") + yonduE;

    const winningRate =
      ((L + O) * (L + V) * (L + E) * (O + V) * (O + E) * (V + E)) % 100;

    if (winningRate > maxRate) {
      maxRate = winningRate;
      maxIdx = index;
    }
  });

  console.log(teams[maxIdx]);
}

function getVar(nameArray, target) {
  return nameArray.filter((char) => {
    return char === target;
  }).length;
}

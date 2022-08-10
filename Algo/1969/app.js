/* node.js */

// 1969 DNA

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수
let N, M;

rl.on("line", function (line) {
  if (inputNum == -1) {
    [N, M] = line.split(" ").map((a) => +a);
    inputNum = N;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const DNAList = input;
  const dnaS = [];

  for (let i = 0; i < M; i++) {
    let [A, T, G, C] = [0, 0, 0, 0];
    for (let j = 0; j < N; j++) {
      const DNA = DNAList[j];
      if (DNA[i] == "A") A++;
      else if (DNA[i] == "T") T++;
      else if (DNA[i] == "G") G++;
      else C++;
    }
    const indexOfMax = [A, C, G, T].findIndex((a) => a == Math.max(A, C, G, T));
    dnaS.push(["A", "C", "G", "T"][indexOfMax]);
  }

  let result = 0;
  for (let i = 0; i < N; i++) {
    result += calcHammingDistance(DNAList[i], dnaS);
  }
  console.log(dnaS.join(""));
  console.log(result);
}

function calcHammingDistance(dnaA, dnaB) {
  let distance = 0;
  for (let i = 0; i < dnaA.length; i++) {
    if (dnaA[i] != dnaB[i]) distance++;
  }
  return distance;
}

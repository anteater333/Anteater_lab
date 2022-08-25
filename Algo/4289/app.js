/* node.js */

// 4289 Rock-Paper-Scissors Tournament

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (line == "0") rl.close();
  input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  for (let i = 0; i < input.length; ) {
    const [n, k] = input[i++].split(" ").map((a) => +a);
    const numOfGames = (k * n * (n - 1)) / 2;

    const players = Array.from(Array(n), () => new Array(2).fill(0));
    for (let j = 0; j < numOfGames; j++, i++) {
      let [player1, hand1, player2, hand2] = input[i].split(" ");
      player1 -= 1;
      player2 -= 1;
      const rcpResult = judgeRSP(hand1, hand2);
      if (rcpResult[0] === 1) {
        players[player1][0] += 1;
        players[player2][1] += 1;
      } else if (rcpResult[0] === -1) {
        players[player2][0] += 1;
        players[player1][1] += 1;
      }
    }
    players.forEach(([w, l]) => {
      const winRate = w / (w + l);
      if (isNaN(winRate)) console.log("-");
      else console.log((Math.round((w / (w + l)) * 1000) / 1000).toFixed(3));
    });
    console.log();
  }
}

function judgeRSP(player1, player2) {
  const result = [0, 0]; // -1 : lost, 0: draw, 1: win
  if (player1 === player2) {
    return result;
  }

  if (
    (player1 === "rock" && player2 === "scissors") ||
    (player1 === "scissors" && player2 === "paper") ||
    (player1 === "paper" && player2 === "rock")
  ) {
    result[0] = 1;
  } else {
    result[0] = -1;
  }
  result[1] = -result[0];

  return result;
}

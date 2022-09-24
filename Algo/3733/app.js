// 3733 Shares

const fs = require("fs");

const input = fs.readFileSync("/dev/stdin").toString().split("\n");
// const input = fs.readFileSync("./input.txt").toString().split("\n");

for (let i = 0; i < input.length; i++) {
  const [N, S] = input[i].split(" ").map((a) => +a);

  const ans = Math.floor(S / (N + 1));

  if (!isNaN(ans)) console.log(ans);
}

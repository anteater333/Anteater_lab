/* node.js */

// 2975 Transactions

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐

rl.on("line", function (line) {
  if (line === "0 W 0") rl.close();
  input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  input.forEach((TC) => {
    let [starting, type, amount] = TC.split(" ");
    starting = +starting;
    amount = +amount;

    const balance = type === "W" ? starting - amount : starting + amount;

    console.log(balance < -200 ? "Not allowed" : balance);
  });
}

/* node.js */

// 1259 팰린드롬수

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
  terminal: false,
});

const output = [];

rl.on("line", function (line) {
  if (line === "0") rl.close();
  output.push(algorila(line));
}).on("close", function () {
  console.log(output.join("\n"));
  process.exit();
});

function algorila(numStr) {
  if (numStr.split("").reverse().join("") == numStr) return "yes";
  return "no";
}

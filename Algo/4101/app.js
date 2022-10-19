/* node.js */

// 4101 크냐?

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

rl.on("line", function (line) {
  const [a, b] = line.split(" ").map((a) => +a);
  if (a === 0 && b === 0) {
    rl.close();
  } else if (a > b) {
    console.log("Yes");
  } else {
    console.log("No");
  }
}).on("close", function () {
  process.exit();
});

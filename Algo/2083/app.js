/* node.js */

// 2083 럭비 클럽

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐

rl.on("line", function (line) {
  if (line === "# 0 0") rl.close();
  else input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const result = [];

  input.forEach((person) => {
    let [name, a, w] = person.split(" ");
    w = +w;
    a = +a;

    if (w >= 80 || a > 17) {
      result.push(`${name} Senior`);
    } else {
      result.push(`${name} Junior`);
    }
  });

  console.log(result.join("\n"));
}

/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐

rl.on("line", function (line) {
  if (line === ".") rl.close();
  else input.push(line);
}).on("close", function () {
  algorila();

  process.exit();
});

function checker(line) {
  let good = true;

  const stack = [];
  let head = 0;

  line.split("").forEach((char) => {
    if (char === "(" || char === "[") {
      stack[head++] = char;
    } else if (char === ")" || char === "]") {
      if (head == 0) {
        good = false;
        return;
      }
      const bracket = stack[--head];
      if (
        !(
          (bracket === "(" && char === ")") ||
          (bracket === "[" && char === "]")
        )
      ) {
        good = false;
        return;
      }
    }
  });

  if (head != 0) good = false;

  return good;
}

function algorila() {
  const result = [];

  input.forEach((line) => {
    result.push(checker(line) ? "yes" : "no");
  });

  console.log(result.join("\n"));
}

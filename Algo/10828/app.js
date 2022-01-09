/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = line;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

const stack = [];
let head = 0;
const result = [];

/** I DO NOT WANT TO USE ANY SWITCH STATEMENT */
const stackMethods = {
  push: (X) => {
    stack[head++] = X;
  },
  pop: () => {
    if (head == 0) {
      result[result.length] = -1;
      return;
    }
    result[result.length] = stack[--head];
    return;
  },
  size: () => {
    result[result.length] = head;
  },
  empty: () => {
    result[result.length] = head ? 0 : 1;
  },
  top: () => {
    if (head == 0) {
      result[result.length] = -1;
      return;
    }
    result[result.length] = stack[head - 1];
  },
};

function interpreter(cmd) {
  const parsedCmd = cmd.split(" ");

  stackMethods[parsedCmd[0]](parsedCmd[1]);
}

function algorila() {
  input.forEach((element) => {
    interpreter(element);
  });

  console.log(result.join("\n"));
}

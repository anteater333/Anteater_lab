const input = require("fs").readFileSync("input.txt").toString().split("\n");

const Q = [];
const result = [];
let frontCursor = 0;
let backCursor = 0;

for (let i = 1; i < input.length; i++) {
  const cmd = input[i];
  /** CMD Interpreter */
  switch (cmd) {
    case "pop": // Actually, it's dequeue
      if (frontCursor == backCursor) result.push(`${-1}`);
      else result.push(`${Q[frontCursor++]}`);
      break;
    case "size":
      result.push(`${backCursor - frontCursor}`);
      break;
    case "empty":
      if (backCursor - frontCursor) result.push(`0`);
      else result.push(`1`);
      break;
    case "front":
      const front = Q[frontCursor];
      if (front) result.push(`${front}`);
      else result.push(`-1`);
      break;
    case "back":
      if (backCursor <= frontCursor) result.push(`-1`);
      else {
        result.push(`${Q[backCursor - 1]}`);
      }
      break;
    default:
      Q[backCursor++] = cmd.split(" ")[1];
  }
}

console.log(result.join("\n"));

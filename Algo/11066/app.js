/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = +line * 2;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const N = inputNum;

  for (let i = 0; i < inputNum; i += 2) {
    const k = +input[i];
    const files = input[i + 1].split(" ").map((a) => +a);

    console.log(concatFiles(files));
  }
}

/**
 * 풀지 못했다
 * https://developerbee.tistory.com/97
 * 공부하자
 */

function concatFiles(files) {
  const filesNum = files.length;

  const memo = Array.from(Array(filesNum), (_) => Array(filesNum).fill(0));
  const sum = [files[0]];
  for (let i = 1; i < filesNum; i++) {
    sum[i] = sum[i - 1] + files[i];
  }

  for (let i = 0; i < filesNum - 1; i++) {
    memo[i][i + 1] = files[i] + files[i + 1];
  }

  // console.log(memo.join("\n"));

  for (let j = 2; j < memo.length; j++) {
    for (let i = 0; i + j < memo.length; i++) {
      for (let k = i; k < i + j; k++) {
        const sumDist = i != 0 ? sum[i + j] - sum[i - 1] : sum[i + j];
        if (memo[i][i + j] == 0)
          memo[i][i + j] = memo[i][k] + memo[k + 1][i + j] + sumDist;
        else
          memo[i][i + j] = Math.min(
            memo[i][i + j],
            memo[i][k] + memo[k + 1][i + j] + sumDist
          );
      }
    }
  }

  // console.log(memo.join("\n"));

  return memo[0][filesNum - 1];
}

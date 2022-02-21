/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

let words = [];

rl.on("line", function (line) {
  if (inputNum == -1) {
    if (line == "-") inputNum++;
    else words.push(cntAlpha(line));
  } else {
    if (line == "#") rl.close();
    else input.push(cntAlpha(line));
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  input.forEach((board) => {
    // console.log(getPossible(board, words));
    // console.log(board);
    // console.log(calcMinMax(board, getPossible(board, words)));
    // console.log("====");
    console.log(calcMinMax(board, getPossible(board, words)).join(" "));
  });
}

function cntAlpha(word) {
  const count = {};
  word.split("").forEach((a) => {
    if (!count[a]) {
      count[a] = 1;
    } else count[a]++;
  });

  return count;
}

function getPossible(board, words) {
  const possibleList = [];

  words.forEach((word) => {
    let isPossible = true;
    for (const [char, cnt] of Object.entries(word)) {
      if (!board[char]) {
        // 퍼즐판에 해당 문자 없음
        isPossible = false;
        break;
      }
      if (cnt > board[char]) {
        // 퍼즐판에 해당 문자 적음
        isPossible = false;
        break;
      }
    }
    if (isPossible) possibleList.push(word);
  });

  return possibleList;
}

function calcMinMax(board, possibleList) {
  const counts = {};

  for (const [char, cnt] of Object.entries(board)) {
    counts[char] = 0;
    for (let i = 0; i < possibleList.length; i++) {
      const word = possibleList[i];
      if (!word[char]) {
        continue;
      }
      if (word[char] > cnt) {
        continue;
      }
      counts[char]++;
    }
  }

  const min = ["", Infinity];
  const max = ["", 0];
  for (const [char, cnt] of Object.entries(counts)) {
    if (cnt < min[1]) {
      min[0] = char;
      min[1] = cnt;
    } else if (cnt == min[1]) {
      min[0] += char;
    }

    if (cnt > max[1]) {
      max[0] = char;
      max[1] = cnt;
    } else if (cnt == max[1]) {
      max[0] += char;
    }
  }

  min[0] = min[0].split("").sort().join("");
  max[0] = max[0].split("").sort().join("");

  return [min[0], min[1], max[0], max[1]];
}

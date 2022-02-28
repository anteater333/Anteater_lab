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

function algorila() {
  const N = +inputNum;
  const friendMap = Array.from({ length: N }, (_, idx) => {
    return input[idx].split("");
  });

  const friendsCnts = [];
  for (let i = 0; i < N; i++) {
    const friends = new Set();
    for (let j = 0; j < N; j++) {
      if (friendMap[i][j] == "Y") {
        friends.add(j);
        for (let k = 0; k < N; k++) {
          if (friendMap[j][k] == "Y") {
            friends.add(k);
          }
        }
      }
    }
    friends.delete(i);
    friendsCnts.push(friends.size);
  }

  console.log(Math.max(...friendsCnts));
}

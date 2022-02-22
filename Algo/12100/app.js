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
  const puzzleBoard = [];

  input.forEach((row, idx) => {
    puzzleBoard[idx] = row.split(" ").map((a) => +a);
  });

  const PS = new Puzzle();
  const stateQue = [[puzzleBoard, 0]];
  let max = 0;
  let head = 0;
  while (head < stateQue.length) {
    const [board, depth] = stateQue[head++];

    const curVal = PS.findMax(board); // 현재 상태에서 최대 크기 블록 확인
    max = curVal > max ? curVal : max;

    if (!PS.checkPromising(board) || depth == 5) {
      // 유망하지 않음 OR 5회 이동 종료
      // 추가 상태 생성 X
    } else {
      // 유망함
      const up = PS.up(board);
      const down = PS.down(board);
      const left = PS.left(board);
      const right = PS.right(board);

      // 해당 방향으로 이동 가능한지 파악
      if (up[1]) {
        stateQue[stateQue.length] = [up[0], depth + 1];
      }
      if (down[1]) {
        stateQue[stateQue.length] = [down[0], depth + 1];
      }
      if (left[1]) {
        stateQue[stateQue.length] = [left[0], depth + 1];
      }
      if (right[1]) {
        stateQue[stateQue.length] = [right[0], depth + 1];
      }
    }
  }

  console.log(max);
}

function Puzzle() {
  this.findMax = function (state) {
    let max = 0;
    for (let i = 0; i < state.length; i++) {
      for (let j = 0; j < state.length; j++) {
        if (state[i][j] != 0) {
          max = state[i][j] > max ? state[i][j] : max;
        }
      }
    }
    return max;
  };

  this.checkPromising = function (state) {
    // 같은 종류의 블록이 1개 밖에 없으면 유망하지 않음.
    const blockCnt = new Array(50).fill(0);

    for (let i = 0; i < state.length; i++) {
      for (let j = 0; j < state.length; j++) {
        if (state[i][j] != 0) {
          blockCnt[Math.log2(state[i][j])]++;
        }
      }
    }

    return blockCnt.some((cnt) => cnt > 1);
  };

  this.up = function (state) {
    const newState = JSON.parse(JSON.stringify(state));

    let moved = false;

    for (let i = 0; i < state.length; i++) {
      let stopIdx = 0;
      for (let j = 0; j < state.length; j++) {
        if (newState[j][i] == 0) {
          continue;
        } else if (stopIdx == j) {
          continue;
        } else if (newState[stopIdx][i] == newState[j][i]) {
          // 합체 가능
          newState[stopIdx][i] += newState[j][i];
          newState[j][i] = 0;
          stopIdx++;
          moved = true;
        } else if (newState[stopIdx][i] == 0) {
          // 블록 이동
          newState[stopIdx][i] = newState[j][i];
          newState[j][i] = 0;
          moved = true;
        } else {
          // 합체 불가능, 블록 바로 아래까지 이동
          stopIdx++;
          if (stopIdx != j) {
            newState[stopIdx][i] = newState[j][i];
            newState[j][i] = 0;
            moved = true;
          }
        }
      }
    }

    return [newState, moved];
  };

  this.down = function (state) {
    const newState = JSON.parse(JSON.stringify(state));

    let moved = false;

    for (let i = 0; i < state.length; i++) {
      let stopIdx = state.length - 1;
      for (let j = state.length - 1; j >= 0; j--) {
        if (newState[j][i] == 0) {
          continue;
        } else if (stopIdx == j) {
          continue;
        } else if (newState[stopIdx][i] == newState[j][i]) {
          // 합체 가능
          newState[stopIdx][i] += newState[j][i];
          newState[j][i] = 0;
          stopIdx--;
          moved = true;
        } else if (newState[stopIdx][i] == 0) {
          // 블록 이동
          newState[stopIdx][i] = newState[j][i];
          newState[j][i] = 0;
          moved = true;
        } else {
          // 합체 불가능, 블록 바로 위까지 이동
          stopIdx--;
          if (stopIdx != j) {
            newState[stopIdx][i] = newState[j][i];
            newState[j][i] = 0;
            moved = true;
          }
        }
      }
    }

    return [newState, moved];
  };

  this.left = function (state) {
    const newState = JSON.parse(JSON.stringify(state));

    let moved = false;

    for (let i = 0; i < state.length; i++) {
      let stopIdx = 0;
      for (let j = 0; j < state.length; j++) {
        if (newState[i][j] == 0) {
          continue;
        } else if (stopIdx == j) {
          continue;
        } else if (newState[i][stopIdx] == newState[i][j]) {
          // 합체 가능
          newState[i][stopIdx] += newState[i][j];
          newState[i][j] = 0;
          stopIdx++;
          moved = true;
        } else if (newState[i][stopIdx] == 0) {
          // 블록 이동
          newState[i][stopIdx] = newState[i][j];
          newState[i][j] = 0;
          moved = true;
        } else {
          // 합체 불가능, 블록 바로 옆까지 이동
          stopIdx++;
          if (stopIdx != j) {
            newState[i][stopIdx] = newState[i][j];
            newState[i][j] = 0;
            moved = true;
          }
        }
      }
    }

    return [newState, moved];
  };

  this.right = function (state) {
    const newState = JSON.parse(JSON.stringify(state));

    let moved = false;

    for (let i = 0; i < state.length; i++) {
      let stopIdx = state.length - 1;
      for (let j = state.length - 1; j >= 0; j--) {
        if (newState[i][j] == 0) {
          continue;
        } else if (stopIdx == j) {
          continue;
        } else if (newState[i][stopIdx] == newState[i][j]) {
          // 합체 가능
          newState[i][stopIdx] += newState[i][j];
          newState[i][j] = 0;
          stopIdx--;
          moved = true;
        } else if (newState[i][stopIdx] == 0) {
          // 블록 이동
          newState[i][stopIdx] = newState[i][j];
          newState[i][j] = 0;
          moved = true;
        } else {
          // 합체 불가능, 블록 바로 옆까지 이동
          stopIdx--;
          if (stopIdx != j) {
            newState[i][stopIdx] = newState[i][j];
            newState[i][j] = 0;
            moved = true;
          }
        }
      }
    }

    return [newState, moved];
  };
}

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
  const resultBuffer = [];
  input.forEach((TC) => {
    resultBuffer[resultBuffer.length] = calcMinJumps(
      ...TC.split(" ").map((a) => +a)
    );
  });

  console.log(resultBuffer.join("\n"));
}

function calcMinJumps(x, y) {
  const start = x;
  const destination = y;
  /** 거리 */
  const distance = destination - start;
  /** 중간지점 */
  const middle = distance / 2;
  let k = 1;
  let current = k;
  let count = 1;

  dashboard(k, current, distance, "taking off");

  /** 운행 시작 */
  while (current != distance) {
    if (current < middle) {
      // 중간지점 통과 이전, 가속
      if ((k + 1) / 2 + current <= middle) {
        k++;
        dashboard(k, current + k, distance, "accelerating");
      } else {
        dashboard(k, current + k, distance, "sustaining");
      }
    } else if (current == middle) {
      dashboard(k, current + k, distance, "sustaining, middle point");
    } else if (current > middle) {
      // 중간지점 통과 이후, 감속
      if (predict(k) > distance - current) {
        k = k <= 1 ? 1 : k - 1;

        dashboard(k, current + k, distance, "decelerating");
      } else dashboard(k, current + k, distance, "sustaining");
    }
    current += k;
    count++;

    if (current > distance) {
      dashboard(k, current, distance, "failed");
      break;
    }
  }

  return count;
}

/**
 *
 * @param {*} jumped 이동 거리
 * @param {*} current 현재 위치
 * @param {*} destination 목적지
 * @param {*} msg 메세지
 */
function dashboard(jumped, current, destination, msg) {
  console.log(
    `jumped ${jumped}, current ${current}, destination ${destination}, remaining ${
      destination - current
    }, prediction ${predict(jumped)} - ${msg}`
  );
}

/**
 * 등차 감속 했을 시 예상 소요
 */
function predict(currentSpeed) {
  return currentSpeed * ((currentSpeed + 1) / 2);
}

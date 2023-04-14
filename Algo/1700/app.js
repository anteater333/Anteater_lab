/* node.js */

// 1700 멀티탭 스케줄링

const { createReadStream } = require("fs");

const rl = require("readline").createInterface({
  input: process.stdin,
  // input: createReadStream("./tc.txt"),
  output: process.stdout,
  terminal: false,
});

/** @type {Array<string>} */
let input = []; // 입력 큐
/** @type {number} */
let inputNum = 2; // 입력 개수

rl.on("line", function (line) {
  input.push(line);
  if (input.length == inputNum) rl.close();
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  const [N, K] = input[0].split(" ").map((a) => +a);
  if (N >= K) {
    // 멀티탭 구멍 수 >= 전기용품 수
    console.log(0);
    return;
  }

  /** 전기용품의 등장 순서 */
  const orderList = input[1].split(" ").map((a) => +a - 1); // 인덱싱을 위해 1 빼줌

  /** 각 전기용품 별 추후 등장 순서 사전 */
  const orderDict = [...Array(100)].map((_) => []);
  for (let order = K - 1; order >= 0; order--) {
    // .pop()을 사용할 수 있도록 역순으로 삽입
    const item = orderList[order];
    orderDict[item].push(order);
  }

  /** 멀티탭 */
  const multiPlug = [];

  // 시뮬레이션
  /** 콘센트 변경 횟수 카운트 */
  let hitCnt = 0;
  for (let i = 0; i < K; i++) {
    const item = orderList[i];
    if (multiPlug.find((v) => v === item) !== undefined) {
      // 현재 순서의 아이템이 이미 멀티탭에 꽂혀있다면
      orderDict[item].pop(); // 순서 제외 후 다음단계로 넘어감
    } else if (multiPlug.length < N) {
      // 멀티탭에 빈공간이 남아있으면 현재 순서의 아이템을 삽입
      multiPlug.push(item);
      orderDict[item].pop();
    } else {
      let latestItemIndex = 0;
      let latestValue = -1;
      for (let j = 0; j < N; j++) {
        // 현재 멀티탭에 꽂혀있는 전기용품 중 가장 늦게 다시 사용될 아이템을 제거
        const oDictJLen = orderDict[multiPlug[j]].length;
        if (oDictJLen === 0) {
          // 더이상 나올 일 없는 전기용품 우선 제거
          latestItemIndex = j;
          break;
        } else {
          // 가장 나중에 나오는 전기용품 선택
          const curVal = orderDict[multiPlug[j]][oDictJLen - 1];
          if (curVal > latestValue) {
            latestValue = curVal;
            latestItemIndex = j;
          }
        }
      }

      orderDict[item].pop();
      multiPlug[latestItemIndex] = item;
      hitCnt++;
    }
  }
  console.log(hitCnt);
}

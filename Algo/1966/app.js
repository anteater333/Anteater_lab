/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = parseInt(line) * 2;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

function algorila() {
  /** 정답 출력 버퍼 */
  const answer = [];

  for (let i = 0; i < input.length; i = i + 2) {
    /** N : 문서의 개수, M : 답을 구하려는 문서 */
    const [N, M] = input[i].split(" ").map((value) => parseInt(value));
    /** 문서의 Queue */
    const queue = input[i + 1].split(" ").map((priority, index) => {
      return {
        index: index,
        priority: priority,
      };
    });
    /** Head, Tail */
    let head = 0,
      tail = queue.length - 1;

    let printedCnt = 0;
    while (head <= tail) {
      current = queue[head];
      for (let j = head + 1; j <= tail; j++) {
        if (queue[j].priority > current.priority) {
          // 더 높은 우선순위의 문서가 큐에 존재함
          queue[queue.length] = current;
          head++;
          tail++;
          current = null;
          break;
        }
      }
      if (current) {
        // 문서 출력
        printedCnt++;
        if (M == current.index) {
          answer.push(printedCnt);
          break;
        }
        head++;
      }
    }
  }

  console.log(answer.join("\n"));
}

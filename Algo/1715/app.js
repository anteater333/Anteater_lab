/* node.js */

// 1715 카드 정렬하기

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
let inputNum = -1; // 입력 개수

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

class Heap {
  /**
   * @param {(x, y) => boolean} comparer 우선순위 판별 함수
   */
  constructor(comparer) {
    this.data = [];
    this.comparer = comparer ? comparer : (x, y) => x > y;
  }

  // #region Btree operations
  getParentIndex(i) {
    return Math.floor(i - 1 / 2);
  }

  getLeftChildIndex(i) {
    return i * 2 + 1;
  }

  getRightChildIndex(i) {
    return i * 2 + 2;
  }
  // #endregion

  /**
   * 두 인덱스의 요소를 스왑
   * @param {*} xi
   * @param {*} yi
   */
  swap(xi, yi) {
    const tmp = this.data[xi];
    this.data[xi] = this.data[yi];
    this.data[yi] = tmp;
  }

  /** 힙에 새 요소를 추가 */
  push(key) {
    // 밑에서 위로 끌어올리기
    const heapifyUp = () => {
      let currentIndex = this.data.length - 1;

      // 현재 요소가 상위 요소보다 우선순위가 높을 때 까지 연산
      while (
        currentIndex >= 0 &&
        this.comparer(
          this.data[currentIndex],
          this.data[this.getParentIndex(currentIndex)]
        )
      ) {
        this.swap(currentIndex, this.getParentIndex(currentIndex));

        currentIndex = this.getParentIndex(currentIndex);
      }
    };

    this.data[this.data.length] = key; // 배열 끝에 덧붙임
    heapifyUp();
  }

  /** 힙의 최상단 요소를 제거 후 반환 */
  poll() {
    // 위에서 밑으로 끌어내리기
    const heapifyDown = () => {
      let currentIndex = 0;

      while (this.data[this.getLeftChildIndex(currentIndex)] !== undefined) {
        // 왼쪽 자식이 존재하는지 먼저 확인

        let biggerChildIndex =
          this.data[this.getRightChildIndex(currentIndex)] !== undefined &&
          this.comparer(
            this.data[this.getRightChildIndex(currentIndex)],
            this.data[this.getLeftChildIndex(currentIndex)]
          )
            ? this.getRightChildIndex(currentIndex)
            : this.getLeftChildIndex(currentIndex); // 오른쪽 자식이 존재하는지 확인 후 우선순위가 더 높은 자식 할당

        // 자식의 우선순위가 더 높다면 위치 교체
        if (
          this.comparer(this.data[biggerChildIndex], this.data[currentIndex])
        ) {
          this.swap(currentIndex, biggerChildIndex);
          currentIndex = biggerChildIndex;
        } else return; // 부모의 우선순위가 더 높다면 알고리즘 정지
      }
    };

    const maxValue = this.data[0];

    // 최하단 요소를 최상단으로 가져옴
    this.data[0] = this.data[this.data.length - 1];
    this.data.length--;

    heapifyDown();

    return maxValue;
  }
}

function algorila() {
  if (input.length === 1) {
    // 카드팩이 하나만 있다면 정렬할 필요가 없다.
    console.log(0);
    return;
  }

  const cardPacks = input.map((a) => +a).sort((a, b) => b - a);
  const cardPackHeap = new Heap((x, y) => x < y);

  let compSum = 0;
  while (cardPacks.length > 0 || cardPackHeap.data.length > 1) {
    let [cardPackA, cardPackB] = Array(2)
      .fill(0)
      .map((_) => {
        if (cardPacks.length === 0) {
          // 카드팩 리스트에 남아있는게 없으면 무조건 힙에서
          return cardPackHeap.poll();
        }

        if (cardPackHeap.data.length === 0) {
          // 힙에 남아있는게 없으면 무조건 카드팩 리스트에서
          return cardPacks.pop();
        }

        if (cardPacks[cardPacks.length - 1] < cardPackHeap.data[0]) {
          // 카드팩 리스트의 최소값이 힙의 최소값보다 더 작다면 카드팩 리스트에서 가져옴
          return cardPacks.pop();
        } else {
          // 그렇지 않다면 힙에서 가져옴
          return cardPackHeap.poll();
        }
      });

    cardPackA ??= 0;
    cardPackB ??= 0; // undefined인 경우 방지

    compSum += cardPackA + cardPackB;

    cardPackHeap.push(cardPackA + cardPackB);
  }

  console.log(compSum);

  // console.log(cardPackHeap.poll()) // 모두 합친 카드의 수
}

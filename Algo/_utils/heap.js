/**
 * 힙 자료구조
 *
 * 구현일 : 2023-04-11
 * 레퍼런스 : https://algoroot.tistory.com/m/69
 */
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

exports.default = Heap;

class MaxPriorityQueue {
  constructor() {
    this.heap = [];
  }

  // Helper Methods
  getLeftChildIndex(parentIndex) {
    return 2 * parentIndex + 1;
  }

  getRightChildIndex(parentIndex) {
    return 2 * parentIndex + 2;
  }

  getParentIndex(childIndex) {
    return Math.floor((childIndex - 1) / 2);
  }

  hasLeftChild(index) {
    return this.getLeftChildIndex(index) < this.heap.length;
  }

  hasRightChild(index) {
    return this.getRightChildIndex(index) < this.heap.length;
  }

  hasParent(index) {
    return this.getParentIndex(index) >= 0;
  }

  leftChild(index) {
    return this.heap[this.getLeftChildIndex(index)];
  }

  rightChild(index) {
    return this.heap[this.getRightChildIndex(index)];
  }

  parent(index) {
    return this.heap[this.getParentIndex(index)];
  }

  swap(indexOne, indexTwo) {
    const temp = this.heap[indexOne];
    this.heap[indexOne] = this.heap[indexTwo];
    this.heap[indexTwo] = temp;
  }

  peek() {
    if (this.heap.length === 0) {
      return null;
    }
    return this.heap[0];
  }

  // Removing an element will remove the
  // top element with highest priority then
  // heapifyDown will be called
  remove() {
    if (this.heap.length === 0) {
      return null;
    }
    const item = this.heap[0];
    this.heap[0] = this.heap[this.heap.length - 1];
    this.heap.pop();
    this.heapifyDown();
    return item;
  }

  add(item) {
    this.heap.push(item);
    this.heapifyUp();
  }

  heapifyUp() {
    let index = this.heap.length - 1;
    while (this.hasParent(index) && this.parent(index) < this.heap[index]) {
      this.swap(this.getParentIndex(index), index);
      index = this.getParentIndex(index);
    }
  }

  heapifyDown() {
    let index = 0;
    while (this.hasLeftChild(index)) {
      let smallerChildIndex = this.getLeftChildIndex(index);
      if (
        this.hasRightChild(index) &&
        this.rightChild(index) > this.leftChild(index)
      ) {
        smallerChildIndex = this.getRightChildIndex(index);
      }
      if (this.heap[index] > this.heap[smallerChildIndex]) {
        break;
      } else {
        this.swap(index, smallerChildIndex);
      }
      index = smallerChildIndex;
    }
  }
}

class MinPriorityQueue {
  constructor() {
    this.heap = [];
  }

  // Helper Methods
  getLeftChildIndex(parentIndex) {
    return 2 * parentIndex + 1;
  }

  getRightChildIndex(parentIndex) {
    return 2 * parentIndex + 2;
  }

  getParentIndex(childIndex) {
    return Math.floor((childIndex - 1) / 2);
  }

  hasLeftChild(index) {
    return this.getLeftChildIndex(index) < this.heap.length;
  }

  hasRightChild(index) {
    return this.getRightChildIndex(index) < this.heap.length;
  }

  hasParent(index) {
    return this.getParentIndex(index) >= 0;
  }

  leftChild(index) {
    return this.heap[this.getLeftChildIndex(index)];
  }

  rightChild(index) {
    return this.heap[this.getRightChildIndex(index)];
  }

  parent(index) {
    return this.heap[this.getParentIndex(index)];
  }

  swap(indexOne, indexTwo) {
    const temp = this.heap[indexOne];
    this.heap[indexOne] = this.heap[indexTwo];
    this.heap[indexTwo] = temp;
  }

  peek() {
    if (this.heap.length === 0) {
      return null;
    }
    return this.heap[0];
  }

  // Removing an element will remove the
  // top element with highest priority then
  // heapifyDown will be called
  remove() {
    if (this.heap.length === 0) {
      return null;
    }
    const item = this.heap[0];
    this.heap[0] = this.heap[this.heap.length - 1];
    this.heap.pop();
    this.heapifyDown();
    return item;
  }

  add(item) {
    this.heap.push(item);
    this.heapifyUp();
  }

  heapifyUp() {
    let index = this.heap.length - 1;
    while (this.hasParent(index) && this.parent(index) > this.heap[index]) {
      this.swap(this.getParentIndex(index), index);
      index = this.getParentIndex(index);
    }
  }

  heapifyDown() {
    let index = 0;
    while (this.hasLeftChild(index)) {
      let smallerChildIndex = this.getLeftChildIndex(index);
      if (
        this.hasRightChild(index) &&
        this.rightChild(index) < this.leftChild(index)
      ) {
        smallerChildIndex = this.getRightChildIndex(index);
      }
      if (this.heap[index] < this.heap[smallerChildIndex]) {
        break;
      } else {
        this.swap(index, smallerChildIndex);
      }
      index = smallerChildIndex;
    }
  }
}

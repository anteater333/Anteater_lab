/**
 * 힙 자료구조
 *
 * 구현일 : 2023-04-11
 * 레퍼런스 : https://algoroot.tistory.com/m/69
 */
class Heap {
  /**
   *
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

  // 밑에서 위로 끌어올리기
  heapifyUp() {
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
  }

  // 위에서 밑으로 끌어내리기
  heapifyDown() {
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
      if (this.comparer(this.data[biggerChildIndex], this.data[currentIndex])) {
        this.swap(currentIndex, biggerChildIndex);
        currentIndex = biggerChildIndex;
      } else return; // 부모의 우선순위가 더 높다면 알고리즘 정지
    }
  }

  /** 힙에 새 요소를 추가 */
  push(key) {
    this.data[this.data.length] = key; // 배열 끝에 덧붙임
    this.heapifyUp();
  }

  /** 힙의 최상단 요소를 제거 후 반환 */
  poll() {
    const maxValue = this.data[0];

    // 최하단 요소를 최상단으로 가져옴
    this.data[0] = this.data[this.data.length - 1];
    this.data.length--;

    this.heapifyDown();

    return maxValue;
  }
}

exports.default = Heap;

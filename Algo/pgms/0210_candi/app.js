function solution(relation) {
  let answer = 0;

  // cols 생성

  const cols = Array.from({ length: relation[0].length }, () => {
    return Array.from({ length: relation.length });
  });

  for (let i = 0; i < relation.length; i++) {
    const row = relation[i];
    for (let j = 0; j < row.length; j++) {
      cols[j][i] = row[j];
    }
  }

  // 후보키 탐색

  let keyArray = [];
  const searchQue = [];

  for (let i = 0; i < cols.length; i++) {
    searchQue.push([i]);
  }

  while (searchQue.length) {
    const cur = searchQue.shift();
    keyArray.push(cur);
    for (let i = cur[cur.length - 1] + 1; i < cols.length; i++) {
      searchQue.push(cur.concat(i));
    }
  }

  while (keyArray.length) {
    const cur = keyArray.shift();

    if (checkUnique(...cur.map((i) => cols[i]))) {
      answer++;
      keyArray = keyArray.filter((key) => !isSubsetOf(key, cur));
    } else {
    }
  }

  return answer;
}

/**
 * sample이 base의 부분집합인지 판단
 */
function isSubsetOf(base, sample) {
  for (let el of sample) {
    if (!base.includes(el)) {
      return false;
    }
  }
  return true;
}

/**
 * Column의 조합에 대해 Uniqueness를 판단한다.
 */
function checkUnique(...col) {
  const combCol = [];
  col[0].forEach((_, index) => {
    combCol[combCol.length] = JSON.stringify(col.map((a) => a[index]));
  });

  return !combCol.some((x) => {
    return combCol.indexOf(x) !== combCol.lastIndexOf(x);
  });
}

const relation = [
  ["100", "ryan", "music", "2"],
  ["200", "apeach", "math", "2"],
  ["300", "tube", "computer", "3"],
  ["400", "con", "computer", "4"],
  ["500", "muzi", "music", "3"],
  ["600", "apeach", "music", "2"],
];

const relation2 = [
  ["1", "2", "3"],
  ["2", "2", "4"],
  ["3", "2", "1"],
];

console.log(solution(relation2));

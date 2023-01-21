function gcd(a, b) {
  if (!b) {
    return a;
  }
  return gcd(b, a % b);
}

function lcm(a, b) {
  return (a * b) / gcd(a, b);
}

function solution(arr) {
  let nLcm = 0;
  for (let i = 0; i < arr.length - 1; i++) {
    if (!nLcm) nLcm = lcm(arr[i], arr[i + 1]);
    else nLcm = lcm(nLcm, arr[i + 1]);
  }

  return nLcm;
}

const ans = solution([5, 6, 9]);

console.log(ans);

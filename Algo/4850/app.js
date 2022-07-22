/* node.js */

// 4850 Baskets of Gold Coins

const input = require("fs")
  .readFileSync("/dev/stdin")
  .toString()
  .split("\n")
  .slice(0, -1);

input.forEach((TC) => {
  const [N, w, d, result] = TC.split(" ").map((a) => +a);

  const compare = w * ((N * (N - 1)) / 2);

  const basket = (compare - result) / d;

  console.log(basket === 0 ? N : basket);
});

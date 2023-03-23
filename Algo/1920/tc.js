const fs = require("fs");

const pow = 31;
const N = 100000;

const A = Array(N)
  .fill(0)
  .map(() =>
    Math.floor(Math.random() * (2 ** pow - -(2 ** pow)) + -(2 ** pow))
  );

const M = N;

const Q = Array(N)
  .fill(0)
  .map(() =>
    Math.floor(Math.random() * (2 ** pow - -(2 ** pow)) + -(2 ** pow))
  );

fs.writeFileSync("./tc.txt", `${N}\n${A.join(" ")}\n${M}\n${Q.join(" ")}\n`);

const fs = require("fs");

const N = 50;
const M = 10_000;

const max = 500_000;
const min = 1;

const towers = Array(N)
  .fill(0)
  .map((_) => Math.ceil(Math.random() * (max - min + 5000) + min));
const boxes = Array(M)
  .fill(0)
  .map((_) => Math.ceil(Math.random() * (max - min - 5000) + min));

const data = `${N}
${towers.join(" ")}
${M}
${boxes.join(" ")}
`;

fs.writeFile("./tc.txt", data, "utf-8", () => {
  console.log("done");
});

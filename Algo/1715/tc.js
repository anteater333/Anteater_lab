const fs = require("fs");

const N = 100_000;

const max = 1_000;
const min = 1;

const cardPacks = Array(N)
  .fill(0)
  .map((_) => Math.ceil(Math.random() * (max - min) + min));

const data = `${N}
${cardPacks.join("\n")}
`;

fs.writeFile("./tc.txt", data, "utf-8", () => {
  console.log("done");
});

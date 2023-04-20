const fs = require("fs");

const N = 100_000;

const min = -10_000;
const max = 10_000;

const numbers = Array(N)
  .fill(0)
  .map((_, i) => {
    return min + i > max ? max : min + i;
  });

const data = `${N}
${numbers.join("\n")}
`;

fs.writeFile("./tc.txt", data, "utf-8", () => {
  console.log("done");
});

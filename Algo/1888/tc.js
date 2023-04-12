const fs = require("fs");

const M = 1000;
const N = 1000;

const max = 1000;
const min = 1;

const wallMap = [...Array(M)].map((_) =>
  Array(N)
    .fill(0)
    .map((_) => {
      const r = Math.floor(Math.random() * (max - min) + min);
      if (r < 6) {
        return r;
      } else return 0;
    })
);

// const wallMap = [...Array(M)].map((_) => Array(N).fill(0));

const data = `${M} ${N}
${wallMap.map((l) => l.join("")).join("\n")}
`;

fs.writeFile("./tc.txt", data, "utf-8", () => {
  console.log("done");
});

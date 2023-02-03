const fs = require("fs");

let data = "200000\n";
for (let i = 0; i < 200000; i++) {
  data += "1 2\n";
}

fs.writeFileSync("./tc.txt", data);

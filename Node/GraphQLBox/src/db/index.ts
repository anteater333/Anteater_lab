import { Database } from "sqlite3";

import * as sql from "./sql";

const db = new Database(process.env.DATABASE_PATH!);

db.serialize(() => {
  // ref https://www.npmjs.com/package/sqlite3

  console.log("DB :: initializing DB tables");
  db.run(sql.createToDo);
  db.run(sql.createToBuy);

  let tobuyCnt = 0;
  db.all(sql.selectAllFromToBuy, (err: any, rows: any) => {
    if (err) {
      console.error(err);
      process.exit(1);
    }
    tobuyCnt = rows.length;
  });

  let todoCnt = 0;
  db.all(sql.selectAllFromToDo, (err: any, rows: any) => {
    if (err) {
      console.error(err);
      process.exit(1);
    }
    todoCnt = rows.length;

    console.log(`DB :: ${tobuyCnt} ToBuy, ${todoCnt} ToDo`);
  });
});

export default db;

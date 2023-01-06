import { readFileSync } from "fs";

export const createToDo = readFileSync("ql/sql/createToDo.sql").toString();

export const createToBuy = readFileSync("ql/sql/createToBuy.sql").toString();

export const selectAllFromToDo = readFileSync(
  "ql/sql/selectAllFromToDo.sql"
).toString();

export const selectAllFromToBuy = readFileSync(
  "ql/sql/selectAllFromToBuy.sql"
).toString();

export const insertToDo = readFileSync("ql/sql/insertToDo.sql").toString();

export const insertToBuy = readFileSync("ql/sql/insertToDo.sql").toString();

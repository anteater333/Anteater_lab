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

export const insertToBuy = readFileSync("ql/sql/insertToBuy.sql").toString();

export const updateToDo = readFileSync("ql/sql/updateToDo.sql").toString();

export const updateToBuy = readFileSync("ql/sql/updateToBuy.sql").toString();

export const deleteToDo = readFileSync("ql/sql/deleteToDo.sql").toString();

export const deleteToBuy = readFileSync("ql/sql/deleteToBuy.sql").toString();

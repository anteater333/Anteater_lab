import { readFileSync } from "fs";
import { dirname, sep, join } from "path";

const appDir = dirname(require.main!.filename).split(sep);
appDir.pop();

const sqlDir = join(appDir.join(sep), "./ql/sql/");

export const createToDo = readFileSync(
  join(sqlDir, "./createToDo.sql")
).toString();

export const createToBuy = readFileSync(
  join(sqlDir, "./createToBuy.sql")
).toString();

export const selectAllFromToDo = readFileSync(
  join(sqlDir, "./selectAllFromToDo.sql")
).toString();

export const selectAllFromToBuy = readFileSync(
  join(sqlDir, "./selectAllFromToBuy.sql")
).toString();

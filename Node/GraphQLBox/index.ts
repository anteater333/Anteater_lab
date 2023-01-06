import express from "express";
import { createHandler } from "graphql-http/lib/use/express";
import sqlite3 from "sqlite3";

import * as sql from "./src/schema/sql";
import {RootResolverMap, RootSchema} from "./src/schema/gql";

import cors from "cors";

// DB (SQLite)
const db = new sqlite3.Database("./db/to.db.sqlite");

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

db.close();

// build express app

const app = express();

app.use(cors());

app.use(express.json());

app.use(function (req, res, next) {
  console.log(req.url, req.body, res.statusCode);
  next();
});

// API endpoint provides graphql service
app.use(
  "/graphql",
  createHandler({
    schema: RootSchema,
    rootValue: RootResolverMap
    // graphiql: true, // enable graphiql mode, the in-browser graphQL editor
  })
);

app.get("/", (req, res) => {
  res.send({
    title: "Welcome to the Server Dobuy",
  });
});

app.post("/", (req, res) => {
  res.json(req.body);
});

const PORT = 4321;

app.listen(PORT, () => {
  console.log(
    `SERVER :: Running a GraphQL API server at http://localhost:${PORT}/graphql`
  );
});

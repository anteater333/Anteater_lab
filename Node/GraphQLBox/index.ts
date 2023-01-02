import express from "express";
import { buildSchema } from "graphql";
import { createHandler } from "graphql-http/lib/use/express";
import sqlite3 from "sqlite3";

import * as sql from "./ql/sql";

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

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// The GraphQL
const gqlSchema = buildSchema(
  `type Query {
    hello: String
  }`
);

// provides a resolve function for each API endpoint
const root = {
  hello: () => {
    return "Hello world!";
  },
};

// build express app

const app = express();

app.use(cors());

app.use(express.json());

// API endpoint provides graphql service
app.use(
  "/graphql",
  createHandler({
    schema: gqlSchema,
    rootValue: root,
    // graphiql: true, // enable graphiql mode, the in-browser graphQL editor
  })
);

app.get("/", (req, res) => {
  res.send({
    hello: "world",
  });
});

app.post("/", (req, res) => {
  console.log(req.body);
  res.json(req.body);
});

const PORT = 4321;

app.listen(PORT, () => {
  console.log(
    `SERVER :: Running a GraphQL API server at http://localhost:${PORT}/graphql`
  );
});

import express from "express";
import { createHandler } from "graphql-http/lib/use/express";
import dotenv from "dotenv";
import cors from "cors";

// .env
dotenv.config();

import gqlSchema from "./src/controllers/gql";
import ToService from "./src/services/to.service";
import db from "./src/db";

// Services
const toServiceInstance = new ToService(db);

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
    schema: gqlSchema,
    context: { toService: toServiceInstance },
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

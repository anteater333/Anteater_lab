import express from "express";
import { buildSchema } from "graphql";
import { createHandler } from "graphql-http/lib/use/express";

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

// API endpoint provides graphql service
app.use(
  "/graphql",
  createHandler({
    schema: gqlSchema,
    rootValue: root,
    // graphiql: true, // enable graphiql mode, the in-browser graphQL editor
  })
);

const PORT = 4321;

app.listen(PORT, () => {
  console.log(
    `Running a GraphQL API server at http://localhost:${PORT}/graphql`
  );
});

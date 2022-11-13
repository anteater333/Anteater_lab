import express from "express";
import { graphqlHTTP } from "express-graphql";
import { buildSchema } from "graphql";

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
  graphqlHTTP({
    schema: gqlSchema,
    rootValue: root,
    graphiql: true, // enable graphiql mode, the in-browser graphQL editor
  })
);

const PORT = 4321;

app.listen(PORT);

console.log(`Running a GraphQL API server at http://localhost:${PORT}/graphql`);

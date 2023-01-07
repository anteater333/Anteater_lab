import { buildASTSchema, GraphQLTypeResolver } from "graphql";

import { makeExecutableSchema } from "@graphql-tools/schema";

import { readFileSync } from "fs";

import { gql } from "graphql-tag";
import { Database } from "sqlite3";

// TBD: 효율적인 코드 분리 구조 연구

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// Ref https://stackoverflow.com/questions/53984094/notable-differences-between-buildschema-and-graphqlschema
/** The GraphQL root schema **/

export const RootTypeSchema = buildASTSchema(
  gql(readFileSync("ql/gql/schema.gql").toString())
);

// provides a resolve function for each API endpoint
export const RootResolverMap = {
  Query: {
    todo: () => {},
    tobuy: () => {},
  },

  Mutation: {
    createTodo: (parent: any, args: any, contextValue: any, info: any) => {
      const db: Database = contextValue.db;

      db.serialize(() => {
        console.log("DB :: Test code");
        db.all(`select * from tobuy`, (err: any, rows: any) => {
          console.log(err);
          console.log(rows);
        });
      });
    },
  },
};

export default makeExecutableSchema({
  typeDefs: RootTypeSchema,
  resolvers: RootResolverMap,
});

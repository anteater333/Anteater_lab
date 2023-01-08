import { buildASTSchema } from "graphql";

import { makeExecutableSchema } from "@graphql-tools/schema";

import { readFileSync } from "fs";

import { gql } from "graphql-tag";
import ToService from "src/services/to.service";
import { RootResolverMap } from "./resolvers";

export interface GQLContext {
  toService: ToService;
}

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// Ref https://stackoverflow.com/questions/53984094/notable-differences-between-buildschema-and-graphqlschema
/** The GraphQL root schema **/

export const RootTypeSchema = buildASTSchema(
  gql(readFileSync("ql/gql/schema.gql").toString())
);

export default makeExecutableSchema({
  typeDefs: RootTypeSchema,
  resolvers: RootResolverMap,
});

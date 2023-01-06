import {
  buildASTSchema,
  buildSchema,
  GraphQLBoolean,
  GraphQLID,
  GraphQLInputObjectType,
  GraphQLInt,
  GraphQLInterfaceType,
  GraphQLObjectType,
  GraphQLSchema,
  GraphQLString,
} from "graphql";

import { readFileSync } from "fs"

import { gql } from "graphql-tag";

// TBD: 효율적인 코드 분리 구조 연구

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// Ref https://stackoverflow.com/questions/53984094/notable-differences-between-buildschema-and-graphqlschema
/** The GraphQL root schema **/

export const RootSchema = buildASTSchema(gql(readFileSync("ql/gql/schema.gql").toString()));

export default RootSchema;

// provides a resolve function for each API endpoint
export const RootResolverMap = {
  Query: {
    todo: () => {
      console.log('test')
    },
    tobuy: () => {},
  },

  Mutation: {},
};

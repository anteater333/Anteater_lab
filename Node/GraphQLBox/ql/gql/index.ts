import {
  GraphQLID,
  GraphQLInputObjectType,
  GraphQLInt,
  GraphQLObjectType,
  GraphQLSchema,
  GraphQLString,
} from "graphql";

// TBD: 효율적인 코드 분리 구조 연구

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// Ref https://stackoverflow.com/questions/53984094/notable-differences-between-buildschema-and-graphqlschema
/** The GraphQL root schema **/
// const gqlSchema = buildSchema(
//   `type Query {
//     hello: String
//   }`
// );

const gqlSchema = new GraphQLSchema({
  query: new GraphQLObjectType({
    name: "RootQueryType",
    fields: {
      tobuy: {
        type: new GraphQLObjectType({
          name: "ToBuyType",
          fields: {
            id: {
              type: GraphQLID,
            },
            title: {
              type: GraphQLString,
            },
            content: {
              type: GraphQLString,
            },
            cost: {
              type: GraphQLInt,
            },
          },
        }),
        args: {
          id: {
            type: GraphQLID,
          },
        },
      },
    },
  }),
  mutation: new GraphQLObjectType({
    name: "TobuyMutation",
    fields: {
      tobuyCreate: {
        type: new GraphQLObjectType({
          name: "TobuyCreateType",
          fields: {
            title: {
              type: GraphQLString,
            },
            content: {
              type: GraphQLString,
            },
            cost: {
              type: GraphQLInt,
            },
          },
        }),
      },
    },
  }),
});

export default gqlSchema;

// provides a resolve function for each API endpoint
const root = {
  hello: () => {
    return "Hello world!";
  },
};

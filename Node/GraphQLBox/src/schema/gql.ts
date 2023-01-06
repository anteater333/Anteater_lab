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
import { gql } from "graphql-tag";

// TBD: 효율적인 코드 분리 구조 연구

/** ★☆★ express-graphql is DEPRECATED. ★☆★ */
// https://github.com/graphql/graphql-http
// LEARN GraphQL over HTTP

// Ref https://stackoverflow.com/questions/53984094/notable-differences-between-buildschema-and-graphqlschema
/** The GraphQL root schema **/
// const RootSchema = buildSchema(
//   `type Query {
//     hello: String
//   }`
// );

const interfaceql = gql`
  type Query {
    hello: String
  }
`;

const buildTest = buildASTSchema(interfaceql);

const ToInterface = new GraphQLInterfaceType({
  name: "ToInterface",
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
    createdAt: {
      type: GraphQLString,
    },
  },
});

const ToDoType = new GraphQLObjectType({
  name: "ToDoType",
  interfaces: [ToInterface],

  fields: {
    done: {
      type: GraphQLBoolean,
    },
    doneAt: {
      type: GraphQLString,
    },
  },
});

const ToBuyType = new GraphQLObjectType({
  name: "ToBuyType",
  interfaces: [ToInterface],
  fields: {
    cost: {
      type: GraphQLInt,
    },
    bought: {
      type: GraphQLBoolean,
    },
    boughtAt: {
      type: GraphQLString,
    },
  },
});

const RootSchema = new GraphQLSchema({
  query: new GraphQLObjectType({
    name: "RootQueryType",
    fields: {
      tobuy: {
        type: ToBuyType,
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

export default RootSchema;

// provides a resolve function for each API endpoint
const root = {
  hello: () => {
    return "Hello world!";
  },
};

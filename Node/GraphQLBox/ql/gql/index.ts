import {
  GraphQLID,
  GraphQLInt,
  GraphQLObjectType,
  GraphQLSchema,
} from "graphql";

// TBD: 효율적인 코드 분리 구조 연구

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
          },
        }),
      },
    },
  }),
});

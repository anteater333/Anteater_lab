import { GQLContext } from "../gql";

// Typescript는 type-graphQL을 쓰는게 좋다.
// provides a resolve function for each API endpoint
export const RootResolverMap = {
  Query: {
    todo: async (
      parent: any,
      args: { id: number },
      contextValue: GQLContext
    ) => {
      return await contextValue.toService.getTodo(args.id);
    },
    todos: (
      parent: any,
      args: { searchBy: { title?: string } },
      contextValue: GQLContext
    ) => {},
    allTodos: (parent: any, args: {}, contextValue: GQLContext) => {},
    tobuy: (parent: any, args: { id: number }, contextValue: GQLContext) => {},
    tobuys: (
      parent: any,
      args: {
        searchBy: {
          title?: string;
          greaterThan?: number;
          equals?: number[];
          smallerThan?: number;
        };
      },
      contextValue: GQLContext
    ) => {},
    allTobuys: (parent: any, args: {}, contextValue: GQLContext) => {},
    budget: (parent: any, args: {}, contextValue: GQLContext) => {
      return 0;
    },
  },

  Mutation: {
    /** Todo Resolvers */
    todoCreate: async (
      parent: any,
      args: { input: { title: string; content?: string } },
      contextValue: GQLContext
    ) => {
      return { todo: await contextValue.toService.createTodo(args.input) };
    },

    /** Tobuy Resolvers */
    tobuyCreate: (
      parent: any,
      args: { title: string; content?: string },
      contextValue: GQLContext
    ) => {},
  },
};

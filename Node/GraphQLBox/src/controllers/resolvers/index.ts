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
    todos: async (
      parent: any,
      args: { searchBy: { title?: string } },
      contextValue: GQLContext
    ) => {
      /** TBD */
    },
    allTodos: async (parent: any, args: {}, contextValue: GQLContext) => {
      return await contextValue.toService.getAllTodo();
    },
    tobuy: async (
      parent: any,
      args: { id: number },
      contextValue: GQLContext
    ) => {
      return await contextValue.toService.getTobuy(args.id);
    },
    tobuys: async (
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
    ) => {
      /** TBD */
    },
    allTobuys: async (parent: any, args: {}, contextValue: GQLContext) => {
      return await contextValue.toService.getAllTobuy();
    },
    budget: async (parent: any, args: {}, contextValue: GQLContext) => {
      return await contextValue.toService.getTotalCost();
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
    todoUpdateContent: async (
      parent: any,
      args: { input: { id: number; newTitle: string; newContent?: string } },
      contextValue: GQLContext
    ) => {
      return {
        todo: await contextValue.toService.updateTodo({
          id: args.input.id,
          title: args.input.newTitle,
          content: args.input.newContent,
        }),
      };
    },
    todoUpdateDone: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return {
        todo: await contextValue.toService.updateTodo({
          id: args.input.id,
          done: true,
        }),
      };
    },
    todoUpdateDoneAll: async (
      parent: any,
      args: { input: {} },
      contextValue: GQLContext
    ) => {
      /** TBD */
    },
    todoUpdateUndone: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return {
        todo: await contextValue.toService.updateTodo({
          id: args.input.id,
          done: false,
        }),
      };
    },
    todoUpdateUndoneAll: async (
      parent: any,
      args: { input: {} },
      contextValue: GQLContext
    ) => {
      /** TBD */
    },
    todoDelete: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return { todo: await contextValue.toService.deleteTodo(args.input.id) };
    },
    /** Tobuy Resolvers */
    tobuyCreate: async (
      parent: any,
      args: { input: { title: string; content?: string } },
      contextValue: GQLContext
    ) => {
      return { todo: await contextValue.toService.createTobuy(args.input) };
    },
    tobuyUpdateContent: async (
      parent: any,
      args: {
        input: {
          id: number;
          newTitle: string;
          newContent?: string;
          newCost: number;
        };
      },
      contextValue: GQLContext
    ) => {
      return {
        tobuy: await contextValue.toService.updateTobuy({
          id: args.input.id,
          title: args.input.newTitle,
          content: args.input.newContent,
          cost: args.input.newCost,
        }),
      };
    },
    tobuyUpdateBought: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return {
        tobuy: await contextValue.toService.updateTobuy({
          id: args.input.id,
          bought: true,
        }),
      };
    },
    tobuyUpdateBoughtAll: async (
      parent: any,
      args: { input: {} },
      contextValue: GQLContext
    ) => {
      /** TBD */
    },
    tobuyUpdateUnbought: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return {
        tobuy: await contextValue.toService.updateTobuy({
          id: args.input.id,
          bought: false,
        }),
      };
    },
    tobuyUpdateUnboughtAll: async (
      parent: any,
      args: { input: {} },
      contextValue: GQLContext
    ) => {
      /** TBD */
    },
    tobuyDelete: async (
      parent: any,
      args: { input: { id: number } },
      contextValue: GQLContext
    ) => {
      return { tobuy: await contextValue.toService.deleteTobuy(args.input.id) };
    },
  },
};

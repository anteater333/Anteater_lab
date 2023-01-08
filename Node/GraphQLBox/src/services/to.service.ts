import { Database } from "sqlite3";
import * as sql from "../db/sql";
import { Tobuy, Todo } from "src/vo/to.vo";
import moment from "moment";

/**
 * TBD: 서비스가 직접 SQLite를 쓰지 않도록 하기
 * 근데 이만한 사이즈 프로젝트 만드는데 굳이...
 */

export default class ToService {
  private readonly db: Database;
  constructor(db: Database) {
    this.db = db;
  }

  /** ToDo */
  public async getTodo(id: number): Promise<Todo> {
    const foundTodo = await new Promise((resolve, reject) => {
      this.db.all(sql.selectToDoById, [id], (err, rows) => {
        if (err) reject(err);
        else if (!rows.length) reject({ message: "NOT_FOUND" });
        else resolve(rows[0]);
      });
    });
    return foundTodo as Todo;
  }

  public async getAllTodo(): Promise<Todo[]> {
    const todoList = await new Promise((resolve, reject) => {
      this.db.all(sql.selectAllFromToDo, (err, rows) => {
        if (err) reject(err);
        else resolve(rows);
      });
    });

    return todoList as Todo[];
  }

  public async createTodo(newTodo: Todo): Promise<Todo> {
    const insertedTodo: Todo = {
      ...newTodo,
      createdAt: moment().format(),
      done: false,
    };
    await new Promise((resolve, reject) => {
      this.db.run(
        sql.insertToDo,
        [
          insertedTodo.title,
          insertedTodo.content,
          insertedTodo.createdAt,
          insertedTodo.done,
        ],
        (err) => {
          if (err) reject(err);
          else resolve(true);
        }
      );
    });
    return insertedTodo;
  }

  public async updateTodo(newTodo: {
    id: number;
    title?: string;
    content?: string;
    done?: boolean;
  }) {
    if (newTodo.title === "") {
      throw { message: "EMPTY_TITLE" };
    }

    const prevTodo = await this.getTodo(newTodo.id);

    const updateTodo: Todo = {
      title: newTodo.title ? newTodo.title : prevTodo.title,
      content: newTodo.content ? newTodo.content : prevTodo.content,
      done: newTodo.done !== undefined ? newTodo.done : prevTodo.done,
      doneAt:
        newTodo.done === true
          ? moment().format()
          : newTodo.done === false
          ? undefined
          : prevTodo.doneAt,
    };

    await new Promise((resolve, reject) => {
      this.db.run(
        sql.updateToDo,
        [
          updateTodo.title,
          updateTodo.content,
          updateTodo.done,
          updateTodo.doneAt,
          newTodo.id,
        ],
        function (err) {
          if (err) reject(err);
          resolve(true);
        }
      );
    });

    const updatedTodo = await this.getTodo(newTodo.id);

    return updatedTodo;
  }

  public async deleteTodo(id: number): Promise<Todo> {
    const targetTodo = await this.getTodo(id);

    await new Promise((resolve, reject) => {
      this.db.run(sql.deleteToDo, [id], function (err) {
        if (err) reject(err);
        else resolve(true);
      });
    });

    return targetTodo;
  }

  /** ToBuy */
  public async getTobuy(id: number): Promise<Tobuy> {
    const foundTobuy = await new Promise((resolve, reject) => {
      this.db.all(sql.selectToBuyById, [id], (err, rows) => {
        if (err) reject(err);
        else if (!rows.length) reject({ message: "NOT_FOUND" });
        else resolve(rows[0]);
      });
    });

    return foundTobuy as Tobuy;
  }

  public async getAllTobuy(): Promise<Tobuy[]> {
    const tobuyList = await new Promise((resolve, reject) => {
      this.db.all(sql.selectAllFromToBuy, (err, rows) => {
        if (err) reject(err);
        else resolve(rows);
      });
    });

    return tobuyList as Tobuy[];
  }

  public async getTotalCost(): Promise<number> {
    const tobuyList = await this.getAllTobuy();

    let total = 0;
    tobuyList.forEach((tobuy) => {
      total += tobuy.cost ? tobuy.cost : 0;
    });

    return total;
  }

  public async createTobuy(newTobuy: Tobuy): Promise<Tobuy> {
    const insertedTobuy: Tobuy = {
      ...newTobuy,
      createdAt: moment().format(),
      bought: false,
    };

    await new Promise((resolve, reject) => {
      this.db.run(
        sql.insertToBuy,
        [
          insertedTobuy.title,
          insertedTobuy.content,
          insertedTobuy.cost,
          insertedTobuy.createdAt,
          insertedTobuy.bought,
        ],
        (err) => {
          if (err) reject(err);
          else resolve(true);
        }
      );
    });
    return insertedTobuy;
  }

  public async updateTobuy(newTobuy: {
    id: number;
    title?: string;
    content?: string;
    cost?: number;
    bought?: boolean;
  }): Promise<Tobuy> {
    if (newTobuy.title === "") {
      throw { message: "EMPTY_TITLE" };
    }

    const prevTobuy = await this.getTobuy(newTobuy.id);

    const updateTodo: Tobuy = {
      title: newTobuy.title ? newTobuy.title : prevTobuy.title,
      content: newTobuy.content ? newTobuy.content : prevTobuy.content,
      cost: newTobuy.cost ? newTobuy.cost : prevTobuy.cost,
      bought: newTobuy.bought ? newTobuy.bought : prevTobuy.bought,
      boughtAt:
        newTobuy.bought === true
          ? moment().format()
          : newTobuy.bought == false
          ? undefined
          : prevTobuy.boughtAt,
    };

    await new Promise((resolve, reject) => {
      this.db.run(
        sql.updateToBuy,
        [
          updateTodo.title,
          updateTodo.content,
          updateTodo.cost,
          updateTodo.bought,
          updateTodo.boughtAt,
          newTobuy.id,
        ],
        function (err) {
          if (err) reject(err);
          resolve(true);
        }
      );
    });

    const updatedTobuy = await this.getTobuy(newTobuy.id);

    return updatedTobuy;
  }

  public async deleteTobuy(id: number): Promise<Tobuy> {
    const targetTobuy = await this.getTobuy(id);

    await new Promise((resolve, reject) => {
      this.db.run(sql.deleteToBuy, [id], function (err) {
        if (err) reject(err);
        else resolve(true);
      });
    });

    return targetTobuy;
  }
}

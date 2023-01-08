import { Database } from "sqlite3";
import * as sql from "../db/sql";
import { Todo } from "src/vo/to.vo";
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
}

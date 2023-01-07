import { Database } from "sqlite3";

export default class ToService {
  private readonly db: Database;
  constructor(db: Database) {
    this.db = db;
  }
}

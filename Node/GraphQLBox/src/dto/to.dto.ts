export class todoDto {
  title: string;
  content: string;
  createdAt?: string;
  done?: boolean;
  doneAt?: string;
  id?: number;
  constructor(
    title: string,
    content: string,
    createdAt?: string,
    done?: boolean,
    doneAt?: string,
    id?: number
  ) {
    this.title = title;
    this.content = content;
    this.createdAt = createdAt;
    this.done = done;
    this.doneAt = doneAt;
    this.id = id;
  }
}

export class tobuyDto {
  title: string;
  content: string;
  cost: number;
  createdAt?: string;
  bought?: boolean;
  boughtAt?: string;
  id?: number;
  constructor(
    title: string,
    content: string,
    cost: number,
    createdAt?: string,
    bought?: boolean,
    boughtAt?: string,
    id?: number
  ) {
    this.title = title;
    this.content = content;
    this.cost = cost;
    this.createdAt = createdAt;
    this.bought = bought;
    this.boughtAt = boughtAt;
    this.id = id;
  }
}

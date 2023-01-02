create table if not EXISTS tobuy (
  id integer PRIMARY key AUTOINCREMENT,
  title text,
  content text,
  cost int,
  createdAt date,
  bought boolean,
  boughtAt date
)
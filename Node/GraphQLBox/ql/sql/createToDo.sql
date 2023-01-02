create table if not EXISTS todo (
  id integer PRIMARY key AUTOINCREMENT,
  title text,
  content text,
  createdAt date,
  done boolean,
  doneAt date
)
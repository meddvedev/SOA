CREATE TABLE IF NOT EXISTS "user" (
  id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
  email varchar(20) NOT NULL,
  username varchar(100) NOT NULL,
  passwordhash varchar(100) NOT NULL,
  timestamp timestamp default current_timestamp
);

CREATE TABLE IF NOT EXISTS insurance (
  id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
  title varchar(20) NOT NULL,
  category varchar(20) NOT NULL,
  insurance_amount integer NOT NULL,
  user_id uuid not null references "user"(id),
  timestamp timestamp default current_timestamp
);

const { Client } = require('pg');
const { migrate } = require('postgres-migrations');

const client = new Client({
  user: "tax", 
  password: "tax",
  host: "localhost",
  database: "tax_dev",
  port: 5432
})

const runMigrations = async () => {
  await client.connect()
  
  try {
    await migrate({client}, "./migrations")
  } catch (error) {
    throw new Error(error);
  }
  finally {
    await client.end();
  }
}

runMigrations();

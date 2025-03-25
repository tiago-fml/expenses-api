# WORK IN PROGRESS 

#expenses-api



docker run --name my_postgres \
  -e POSTGRES_PASSWORD=mysecretpassword \
  -e POSTGRES_DB=mydatabase \
  -p 5432:5432 \
  -v my_dbdata:/var/lib/postgresql/data \
  -d postgres

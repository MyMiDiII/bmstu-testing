#cat ../sql/init-db.sql | docker exec -i test-testdb-1 psql -U postgres -d testdb
#cat ../sql/fill-db.sql | docker exec -i test-testdb-1 psql -U postgres -d testdb
export PGPASSWORD=postgres
echo "pwd"
pwd
psql -h localhost -U postgres -d testdb -f ../sql/init-db.sql
psql -h localhost -U postgres -d testdb -f ../sql/fill-db.sql

dotnet test --logger "console;verbosity=detailed"

psql -h localhost -U postgres -d testdb -f ../sql/clear-db.sql
psql -h localhost -U postgres -d testdb -f ../sql/drop-db.sql
#cat ../sql/clear-db.sql | docker exec -i test-testdb-1 psql -U postgres -d testdb
#cat ../sql/drop-db.sql | docker exec -i test-testdb-1 psql -U postgres -d testdb
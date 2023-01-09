export PGPASSWORD=postgres
psql -h localhost -U postgres -d testdb -f ../sql/init-db.sql
psql -h localhost -U postgres -d testdb -f ../sql/fill-db.sql

dotnet test --logger "console;verbosity=detailed"
test_exit_code=$?

psql -h localhost -U postgres -d testdb -f ../sql/clear-db.sql
psql -h localhost -U postgres -d testdb -f ../sql/drop-db.sql

exit "$test_exit_code"
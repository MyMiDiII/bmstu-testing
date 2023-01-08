# src-postgres-1 -- название моего докер контейнера для postgres-а
######
cat ./sql/clear-db.sql | docker exec -i src-postgres-1 psql -U amunra23 -d test_db
cat ./sql/drop-db.sql | docker exec -i src-postgres-1 psql -U amunra23 -d test_db
######
cat ./sql/init-db.sql | docker exec -i src-postgres-1 psql -U amunra23 -d test_db
cat ./sql/fill-db.sql | docker exec -i src-postgres-1 psql -U amunra23 -d test_db
dotnet restore
dotnet build --no-restore
dotnet test

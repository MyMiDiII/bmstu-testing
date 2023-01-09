#!/bin/bash

# запускаем контейнер
sudo docker-compose -f dockerdb/postgres.yml up -d
# чтобы контейнер успел инициализироваться
sleep 1.5
# проливаем миграции
python manage.py migrate

# здесь должен быть сбор статитики
# пока просто получение списка стран
psql -h localhost -U postgres -d benchmarkdb -c "select * from country"

# удаляем конейнер
sudo docker-compose -f dockerdb/postgres.yml down

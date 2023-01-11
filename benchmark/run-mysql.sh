#!/bin/bash

# запускаем контейнер
sudo docker-compose -f dockerdb/mysql.yml up -d

# проливаем миграции
echo "Waiting for migrations..."
exit_code=1
while [[ $exit_code -ne 0 ]]
do
    python3 manage.py migrate 2> /dev/null
    exit_code=$?
    sleep 1
done

# здесь должен быть сбор статитики
# пока просто получение списка стран
mysql -h 127.0.0.1 -P 3307 -u root -proot -D benchmarkdb -e "show tables;"

# удаляем конейнер
sudo docker-compose -f dockerdb/mysql.yml down

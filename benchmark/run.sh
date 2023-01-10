DATABASES="postgres mysql"

migrate() {
    echo "Waiting for migrations..."
    exit_code=1
    while [[ $exit_code -ne 0 ]]
    do
        python3 manage.py migrate 2> /dev/null
        exit_code=$?
        sleep 1
    done
}

for DATABASE in $DATABASES
  do
    echo "$DATABASE"
    export DATABASE
    COMPOSE_FILE="dockerdb/$DATABASE.yml"  
    sudo docker-compose -f $COMPOSE_FILE up -d 
    migrate
    python3 manage.py runscript test
    sudo docker-compose -f $COMPOSE_FILE down
    sleep 1
  done
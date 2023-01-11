# Some beauty
SEPARATING_LINE_DB="====================="
SEPARATING_LINE="---------------------"


DATABASES="postgres mysql"
export DATABASES

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

RUNS_NUM=5
export RUNS_NUM

for DATABASE in $DATABASES
do
  echo "$SEPARATING_LINE_DB" 
  echo "Database: $DATABASE"
  echo "$SEPARATING_LINE_DB"

  export DATABASE
  COMPOSE_FILE="dockerdb/$DATABASE.yml"

  for I in $(seq 1 $RUNS_NUM)
  do
    export I

    echo "$SEPARATING_LINE"
    echo "Run: $I"
    echo "$SEPARATING_LINE"

    sudo docker-compose -f $COMPOSE_FILE up -d 
    migrate

    echo "$SEPARATING_LINE"
    python3 manage.py runscript test
    echo "$SEPARATING_LINE"
    
    sudo docker-compose -f $COMPOSE_FILE down -v
    sleep 1
  done
done

python3 manage.py runscript graph

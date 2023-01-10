import os
import csv

def parse_results():
    strDBs = os.getenv("DATABASES")
    databases = strDBs.split()

    strRuns = os.getenv("RUNS_NUM")
    runs_num = int(strRuns)
    
    databases_result = dict()
    for db in databases:
        db_result = dict()
        results_path = f"results/{db}"

        for i in range(1, runs_num + 1):
            with open(f"{results_path}/{str(i).zfill(3)}.csv", "r") as file:
                reader = csv.DictReader(file, fieldnames=["name", "time"], delimiter=";")

                for row in reader:
                    if row['name'] in db_result:
                        db_result[row['name']] += float(row['time'])
                    else:
                        db_result[row['name']] = float(row['time'])
                        
        print(db_result)
        db_result = dict((key, val / runs_num) for key, val in db_result.items())
        print(db_result)

        databases_result[db] = db_result

    print(databases_result)

    return databases_result

    
def graph():
    # построение графиков
    pass

def run():
    results = parse_results()
    graph(results)
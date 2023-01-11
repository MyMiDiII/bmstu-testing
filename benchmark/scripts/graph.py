import os
import csv
import matplotlib.pyplot as plt


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
                        
        db_result = dict((key, val / runs_num) for key, val in db_result.items())
        databases_result[db] = db_result

    return databases_result


def configureAx(ax: plt.Axes, name: str, data: dict):
    postgresResults: dict = data["postgres"]
    mysqlResults: dict = data["mysql"]

    databaseNames = ["postgresql", "mysql"]
    barColors = ["orange", "purple"]

    ax.set_xlabel("Databases")
    ax.set_ylabel("Time")

    ax.set_title(name.upper())

    ax.bar(
        databaseNames,
        [postgresResults[name.lower()], mysqlResults[name.lower()]],
        color=barColors
    )

    return ax

    
def graph(databaseResults: dict):
    # Построение графиков
    fig, axes = plt.subplots(2, 2)

    fig.set_figheight(7)
    fig.set_figwidth(11)
    fig.tight_layout(pad=5) # чтобы графики друг к другу не липли

    axes[0][0] = configureAx(axes[0][0], name="insert", data=databaseResults)
    axes[0][1] = configureAx(axes[0][1], name="get", data=databaseResults)
    axes[1][0] = configureAx(axes[1][0], name="update", data=databaseResults)
    axes[1][1] = configureAx(axes[1][1], name="delete", data=databaseResults)

    fig.savefig("results/pdf/test.pdf")
    plt.show()



def run():
    results = parse_results()
    graph(results)

import os
import shutil

import csv
import time

from servering.models import Country

def test_insert():
    delta_t = 0
    with open("../backend/DataGenerator/data/country_data.csv", "r") as file:
        strCountries = csv.reader(file, delimiter=';')
        countries = [
            Country(name=strCountry[0],
                    level_of_interest=strCountry[1],
                    overall_players=strCountry[2])
            for strCountry in strCountries]

        start_time = time.time()
        Country.objects.bulk_create(countries)
        delta_t = time.time() - start_time

    return {"name" : "insert30", "time" : delta_t}


def clear_dir(dirname):
    for filename in os.listdir(dirname):
        file_path = os.path.join(dirname, filename)
        try:
            if os.path.isfile(file_path) or os.path.islink(file_path):
                os.unlink(file_path)
            elif os.path.isdir(file_path):
                shutil.rmtree(file_path)
        except Exception as e:
            print('Failed to delete %s. Reason: %s' % (file_path, e))


def create_report_file(results):
    index = os.getenv("I")
    current_filename = str(index).zfill(3)

    db = os.getenv("DATABASE")
    result_path = f"results/{db}"
    if not os.path.exists(result_path):
        os.makedirs(result_path)

    if index == "1":
        clear_dir(result_path)

    with open(f"{result_path}/{current_filename}.csv", "w") as result_file:
        field_names = ['name', "time"]
        writer = csv.DictWriter(result_file, fieldnames=field_names, delimiter=";")

        for result in results:
            writer.writerow(result)
    

def run():
    results = []
    insert_time = test_insert()
    results.append(insert_time)

    create_report_file(results)

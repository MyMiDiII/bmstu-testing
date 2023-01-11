import csv
import time

from servering.models import Country

def test_insert():
    print("Test: INSERT")

    delta_t = 0
    with open("./data/country_data.csv", "r") as file:
        strCountries = csv.reader(file, delimiter=';')
        countries = [
            Country(name=strCountry[0],
                    level_of_interest=strCountry[1],
                    overall_players=strCountry[2])
            for strCountry in strCountries]

        start_time = time.time()
        Country.objects.bulk_create(countries)
        delta_t = time.time() - start_time

    return {"name" : "insert", "time" : delta_t}


def test_update():
    print("Test: UPDATE")
    delta_t = 0

    start_time = time.time()
    for i in range(1, 1001):
        Country.objects.filter(id=i).update(overall_players=i)
    delta_t = time.time() - start_time

    return {"name" : "update", "time" : delta_t}


def test_get():
    print("Test: GET")
    delta_t = 0

    start_time = time.time()
    for i in range(1, 1001):
        Country.objects.get(id=i)
    delta_t = time.time() - start_time

    return {"name" : "get", "time" : delta_t}


def test_delete():
    print("Test: DELETE")
    delta_t = 0

    start_time = time.time()
    for i in range(1, 1001):
        Country.objects.filter(id=i).delete()
    delta_t = time.time() - start_time

    return {"name" : "delete", "time" : delta_t}

    
def test_country():
    insert_res = test_insert()
    update_res = test_update()
    get_res = test_get()
    delete_res = test_delete()

    return [insert_res, update_res, get_res, delete_res]

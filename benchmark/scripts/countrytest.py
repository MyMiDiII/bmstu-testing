import csv
import time

from servering.models import Country

def test_insert():
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

        print(Country.objects.count())

    return {"name" : "insert", "time" : delta_t}


def test_delete():
    delta_t = 0

    start_time = time.time()
    for i in range(1, 1001):
        Country.objects.filter(id=i).delete()
    delta_t = time.time() - start_time

    print(Country.objects.count())

    return {"name" : "delete", "time" : delta_t}

    
def test_country():
    insert_res = test_insert()
    delete_res = test_delete()

    return [insert_res, delete_res]
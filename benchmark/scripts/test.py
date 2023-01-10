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

        print("BEFORE")
        print(Country.objects.all())
        
        start_time = time.time()
        Country.objects.bulk_create(countries)
        delta_t = time.time() - start_time

        print("AFTER")
        print(Country.objects.all())

    return delta_t
    

def run():
    insert_time = test_insert()
    print(insert_time)


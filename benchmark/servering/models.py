from django.db import models

class Country(models.Model):
    name = models.CharField(max_length=255)
    level_of_interest = models.IntegerField()
    overall_players = models.IntegerField()

    class Meta:
        db_table = "country"
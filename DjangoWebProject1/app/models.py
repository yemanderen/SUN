"""
Definition of models.
"""

from django.db import models

# Create your models here.
class The_user(models.Model):
    username = models.CharField(max_length=30)
    password = models.CharField(max_length=255)

    class Meta:
        verbose_name_plural = 'test user'

        def __str__(self):
            return self.username
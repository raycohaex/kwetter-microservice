import requests
import json
import random
import time
import lorem
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium import webdriver

# Keycloak server URL
KEYCLOAK_URL = "http://172.24.0.1:8080/auth"

# Microservice server URL
MICROSERVICE_URL = "http://localhost"

# Read user data from a file
with open("users_fuller.json", "r") as f:
    users = json.load(f)


# Standard password for all users
password = "Test123!"

# Create a new user and authenticate them for each user in the list


for user in users:
    name = user["name"]
    email = user["email"]
    firstName = user["firstName"]
    lastName = user["lastName"]


    #Authenticate the user and get an access token
    r = requests.post(
        f"{KEYCLOAK_URL}/realms/master/protocol/openid-connect/token",
        data={"grant_type": "password", "client_id": "kwetter-client", "username": email, "password": password},
        auth=("kwetter-client", "fekfQnIqCqi0s5V8459KFHnkkOqdsHOY")
    )
    r.raise_for_status()
    access_token = r.json()["access_token"]

    # Follow all other users
    # Create 50-150 random tweets
    num_tweets = random.randint(1, 1)
    for i in range(num_tweets):
        r = requests.post(
            f"{MICROSERVICE_URL}/tweet",
            json={"userName": "null", "tweetBody": lorem.sentence()},
            headers={"Authorization": f"Bearer {access_token}"}
        )
        time.sleep(0.03)
        r.raise_for_status()
print("Done!")
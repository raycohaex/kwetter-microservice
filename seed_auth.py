import requests
import json
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
driver = webdriver.Firefox()

for user in users:
    name = user["name"]
    email = user["email"]
    firstName = user["firstName"]
    lastName = user["lastName"]

    # Register an account as the user within the master realm and kwetter-frontend client
    

    # Navigate to the registration page
    driver.get(f"{KEYCLOAK_URL}/realms/master/protocol/openid-connect/registrations?client_id=kwetter-frontend&response_type=code&scope=openid email&redirect_uri=http://localhost")

    # Fill out the registration form
    driver.find_element(By.ID, "firstName").send_keys(firstName)
    driver.find_element(By.ID, "lastName").send_keys(lastName)
    driver.find_element(By.ID, "username").send_keys(name)
    driver.find_element(By.ID, "email").send_keys(email)
    driver.find_element(By.ID, "password").send_keys(password)
    driver.find_element(By.ID, "password-confirm").send_keys(password)

    # Submit the form
    driver.find_element(By.XPATH, "//input[@value='Register']").click()

    driver.delete_all_cookies()


    # Authenticate the user and get an access token
    # r = requests.post(
    #     f"{KEYCLOAK_URL}/realms/master/protocol/openid-connect/token",
    #     data={"grant_type": "password", "client_id": "kwetter-client", "username": email, "password": password},
    #     auth=("kwetter-client", "fekfQnIqCqi0s5V8459KFHnkkOqdsHOY")
    # )
    # r.raise_for_status()
    # access_token = r.json()["access_token"]

    # # Set the access token as a bearer token in the headers

    # # Follow all other users
    # for other_user in users:
    #     if other_user != user:
    #         r = requests.post(
    #             f"{MICROSERVICE_URL}/social/follow",
    #             json=other_user["name"],
    #             headers={"Authorization": f"Bearer {access_token}"}
    #         )
    #         r.raise_for_status()


    # r = requests.post(
    #     f"{MICROSERVICE_URL}/social/follow",
    #     json='travis',
    #     headers={"Authorization": f"Bearer {access_token}"}
    # )
    # r.raise_for_status()


    # # Create 50-150 random tweets
    # import random
    # num_tweets = random.randint(50, 150)
    # for i in range(num_tweets):
    #     r = requests.post(
    #         f"{MICROSERVICE_URL}/tweet",
    #         json={"userName": "null", "tweetBody": lorem.sentence()},
    #         headers={"Authorization": f"Bearer {access_token}"}
    #     )
    #     r.raise_for_status()
driver.quit()
print("Done!")
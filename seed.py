import requests
import json

# Keycloak server URL
KEYCLOAK_URL = "http://localhost:8080/auth"

# Microservice server URL
MICROSERVICE_URL = "http://localhost"

# Read user data from a file
with open("users.json", "r") as f:
    users = json.load(f)

# Standard password for all users
password = "Test123!"

# Create a new user and authenticate them for each user in the list
for user in users:
    name = user["name"]
    email = user["email"]

    # Create a new user
    r = requests.post(
        f"{KEYCLOAK_URL}/admin/realms/myrealm/users",
        json={"username": email, "email": email, "enabled": True, "credentials": [{"type": "password", "value": password}]},
        headers={"Content-Type": "application/json"}
    )
    r.raise_for_status()

    # Authenticate the user and get an access token
    r = requests.post(
        f"{KEYCLOAK_URL}/realms/myrealm/protocol/openid-connect/token",
        data={"grant_type": "password", "client_id": "myclient", "username": email, "password": password},
        auth=("myclient", "secret")
    )
    r.raise_for_status()
    access_token = r.json()["access_token"]

    # Set the access token as a bearer token in the headers
    headers = {"Authorization": f"Bearer {access_token}"}

    # Follow all other users
    for other_user in users:
        if other_user != user:
            r = requests.post(
                f"{MICROSERVICE_URL}/follow",
                json={"id": other_user["id"]},
                headers=headers
            )
            r.raise_for_status()

    # Create 50-150 random tweets
    import random
    num_tweets = random.randint(50, 150)
    for i in range(num_tweets):
        r = requests.post(
            f"{MICROSERVICE_URL}/tweet",
            json={"userName": name, "tweetBody": "Lorem ipsum dolor sit amet"},
            headers=headers
        )
        r.raise_for_status()

print("Done!")
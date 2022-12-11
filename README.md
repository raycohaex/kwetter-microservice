[![raycohaex](https://circleci.com/gh/raycohaex/kwetter-microservice.svg?style=svg)](https://app.circleci.com/pipelines/github/raycohaex/kwetter-microservice)


# Overview
This project is a Twitter clone, following the popular microservice architecture. The primary focus will be on functional requirements, so in this repository you will see minimum functional requirements.

# Gateway endpoints
| Endpoint                            | Upstream HTTP Method | Cache TTL (seconds) | Expected body |
|-------------------------------------|----------------------|--------------------|----------------|
| /tweet                              | POST                 | 30                 | JSON: userName, tweetBody |
| /tweet/{tweetId}                    | GET                  |                     | |
| /user-timeline/{username}           | GET                  | 30                 | |
| /social/follow                       | POST                 | 30                 | JSON: follower(username), followee(username)|
| /social/user                         | POST                 | 30                 | JSON: id(guid), userName|

# Install
## Docker
To spin up all Docker containers locally, run;
```docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d```

To get rid of the containers again, run;
```docker-compose -f docker-compose.yml -f docker-compose.override.yml down```

To setup Keycloak after running docker-compose
```docker exec local_keycloak /opt/jboss/keycloak/bin/add-user-keycloak.sh -u admin -p admin && docker restart local_keycloak```
**note:** manually configure Keycloak.

# Architecture overview
![image](https://user-images.githubusercontent.com/11200658/206924791-c1aa7d65-b7f8-4af7-bb7e-783549c9e957.png)

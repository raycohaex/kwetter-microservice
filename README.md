[![raycohaex](https://circleci.com/gh/raycohaex/kwetter-microservice.svg?style=svg)](https://app.circleci.com/pipelines/github/raycohaex/kwetter-microservice)


# Overview
This project is a Twitter clone, following the popular microservice architecture. The primary focus will be on functional requirements, so in this repository you will see minimum functional requirements.

# Docker
To spin up all Docker containers locally, run;
```docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d```

To get rid of the containers again, run;
```docker-compose -f docker-compose.yml -f docker-compose.override.yml down```

To setup Keycloak after running docker-compose
```docker exec local_keycloak /opt/jboss/keycloak/bin/add-user-keycloak.sh -u admin -p admin && docker restart local_keycloak```
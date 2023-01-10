import Keycloak from 'keycloak-js';

let keycloak = Keycloak({
    url: "http://172.24.0.1:8080/auth",
    realm: "master",
    clientId: "kwetter-frontend"
  });

export default keycloak ;
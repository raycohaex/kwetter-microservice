import Keycloak from 'keycloak-js';

let keycloak = Keycloak({
    url: "http://localhost:8080/auth",
    realm: "kwetter",
    clientId: "kwetter-frontend"
  });

export default keycloak ;
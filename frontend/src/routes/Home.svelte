<script>
    // import the keycloak.js keycloak which is in the root of src
    import keycloak from '../keycloak.js';
    import { Api } from '../api.js';

    let pageName="Home Page";

// get bearer token from keycloak-js
const token = keycloak.token;

const api = new Api('http://localhost:5138/', {
  'Authorization': `Bearer ${token}`,
});

async function getData() {
  const data = await api.get('api/v1/Tweet/08dad221-95d2-4df7-80ac-06f5f689a28e').then(res => console.log(res)).catch(err => console.log(err));
}

getData();
</script>

<main>
    <h1> {pageName}!</h1>
    <p>Welcome this is my <b>{pageName}</b></p>
</main>

<style>
    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
    }

    h1 {
        color: #ff3e00;
        text-transform: uppercase;
        font-size: 4em;
        font-weight: 100;
    }

    @media (min-width: 640px) {
        main {
            max-width: none;
        }
    }
</style>
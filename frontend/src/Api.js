import axios from 'axios';
import keycloak from './keycloak.js';

export const client = axios.create({
  // add content type header
  timeout: 1000,
  headers: {
    'Content-Type': 'application/json',
  },
  baseURL: 'http://localhost/'
});
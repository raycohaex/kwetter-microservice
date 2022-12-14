import axios from 'axios';
import keycloak from './keycloak.js';

export class Api {
    constructor(baseUrl, extraHeaders = {}) {
      this.client = axios.create({
        baseURL: baseUrl,
        // add content-type header to every request
        headers: {
            'Content-Type': 'application/json'
        }
      });
    }
  
    async get(url, config = {}) {

      return await this.client.get(url, config);
    }
  
    post(url, data, config = {}) {
      return this.client.post(url, data, config);
    }
  
    put(url, data, config = {}) {
      return this.client.put(url, data, config);
    }
  
    delete(url, config = {}) {
      return this.client.delete(url, config);
    }
  }
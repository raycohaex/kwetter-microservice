import axios from 'axios';


export class Api {
    constructor(baseUrl, headers = {}) {
      this.client = axios.create({
        baseURL: baseUrl,
        // add content-type header to every request
        headers: {
            'Content-Type': 'application/json',
            ...headers
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
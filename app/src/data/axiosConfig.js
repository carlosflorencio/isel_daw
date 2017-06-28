import axios from 'axios'

/**
 * Axios default config for every request
 * Loaded in index.js
 *
 * More info:
 * https://github.com/mzabriskie/axios
 */
axios.defaults.baseURL = 'http://localhost:5000'
axios.defaults.timeout = 5000
axios.defaults.responseType = 'application/vnd.siren+json'
axios.defaults.headers.common['Authorization'] = 
    localStorage.getItem('jwt') ? 'Basic ' + localStorage.getItem('jwt') : undefined
//axios.defaults.headers.post['Content-Type'] = 'application/json'

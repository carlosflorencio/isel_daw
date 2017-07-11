import axios from 'axios'

export default function (url, config = {}) {
    const initialConfig = {
        baseURL: 'http://localhost:5000',
        method: config.method || 'GET',
        url: url,
        timeout: 5000,
        responseType: 'application/vnd.siren+json',
        headers: {
        },
        params: config.params,
        data: config.data
    }
    if(localStorage.getItem('oidc.user:http://localhost:5000:daw-app')){
        initialConfig.headers.Authorization = 'Bearer ' + 
            JSON.parse(
                localStorage.getItem('oidc.user:http://localhost:5000:daw-app')
            )['access_token']
    }

    return axios.request(initialConfig)
}

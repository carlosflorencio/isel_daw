import axios from 'axios'

export default function (url, method, data) {
    const config = {
        baseURL: 'http://localhost:5000',
        method: method || 'GET',
        url: url,
        timeout: 5000,
        responseType: 'application/vnd.siren+json',
        headers: {
        },
        data: data
    }
    if(localStorage.getItem('oidc.user:http://localhost:5000:daw-app')){
        config.headers.Authorization = 'Bearer ' + 
            JSON.parse(
                localStorage.getItem('oidc.user:http://localhost:5000:daw-app')
            )['access_token']
    }

    return axios.request(config)
}

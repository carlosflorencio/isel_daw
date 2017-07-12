import axios from 'axios'

const BASE_URL = 'http://localhost:5000'

const JWT_NAME = 'oidc.user:' + BASE_URL + ':daw-app'

export default function(url, config = {}) {
  const initialConfig = {
    baseURL: BASE_URL,
    method: config.method || 'GET',
    url: url,
    timeout: 5000,
    responseType: 'application/vnd.siren+json',
    headers: {},
    params: config.params,
    data: config.data
  }

  if (localStorage.getItem(JWT_NAME)) {
    initialConfig.headers.Authorization =
      'Bearer ' + JSON.parse(localStorage.getItem(JWT_NAME))['access_token']
  }

  return axios.request(initialConfig)
}

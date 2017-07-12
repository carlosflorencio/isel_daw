import React from 'react'
import ReactDOM from 'react-dom'
import App from './App'
import registerServiceWorker from './registerServiceWorker'
import './assets/css/main.css'
import './assets/css/pace-theme-simple.css'
import 'semantic-ui-css/semantic.min.css'

ReactDOM.render(<App />, document.getElementById('root'))
registerServiceWorker()

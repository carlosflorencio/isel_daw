import { createStore, applyMiddleware } from 'redux'
import rootReducer from './rootReducer'

import apiCallMiddleware from './middlewares/apiCallMiddleware'

function configureStore(initialState) {
  return createStore(
    rootReducer,
    initialState,
    applyMiddleware(apiCallMiddleware)
  )
}

export default configureStore

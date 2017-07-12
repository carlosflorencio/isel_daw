import { createStore, applyMiddleware } from 'redux'
import rootReducer from './rootReducer'
import thunk from 'redux-thunk'
import apiCallMiddleware from './middlewares/apiCallMiddleware'

// Chrome Redux Devtools
import { composeWithDevTools } from 'redux-devtools-extension'

const middleware = [apiCallMiddleware, thunk]

function configureStore(initialState) {
  return createStore(
    rootReducer,
    initialState,
    composeWithDevTools(applyMiddleware(...middleware))
  )
}

export default configureStore

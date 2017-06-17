import { createStore } from 'redux'
import rootReducer from './rootReducer'

function configureStore (initialState) {
  return createStore(
    rootReducer,
    initialState
  )
}

export default configureStore
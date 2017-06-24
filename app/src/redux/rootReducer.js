import { combineReducers } from 'redux'

import {default as session} from '../components/login/AuthReducer'

// Where we can add more reducers
const rootReducer = combineReducers({
    //Add reducers
    session
})

export default rootReducer

import { combineReducers } from 'redux'

import {default as session} from '../components/auth/AuthReducer'
import {default as api} from '../components/ApiReducer'

// Where we can add more reducers
const rootReducer = combineReducers({
    //Add reducers
    api,
    session
})

export default rootReducer
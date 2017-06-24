import { combineReducers } from 'redux'

import {default as session} from '../components/login/AuthReducer'
import {default as courses} from '../components/courses/CoursesReducer'

// Where we can add more reducers
const rootReducer = combineReducers({
    //Add reducers
    session,
    courses
})

export default rootReducer

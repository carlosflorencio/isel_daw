import * as helpers from '../../helpers/ReduxHelpers'

import CoursesRepository from '../../data/repositories/CoursesRepository'

/*
 |--------------------------------------------------------------------------
 | Types
 |--------------------------------------------------------------------------
 */
export const LOAD_COURSES_REQUEST = 'daw/auth/LOAD_COURSES_REQUEST'
export const LOAD_COURSES_SUCCESS = 'daw/auth/LOAD_COURSES_SUCCESS'
export const LOAD_COURSES_FAILURE = 'daw/auth/LOAD_COURSES_FAILURE'


/*
 |--------------------------------------------------------------------------
 | Reducer
 |--------------------------------------------------------------------------
 */
const initialState = {
}

export default helpers.createReducer(initialState, {
  [LOAD_COURSES_SUCCESS]: (state, action) => {
    return action.response
  },
  [LOAD_COURSES_FAILURE]: (state, action) => {
    return initialState
  }
})

/*
|--------------------------------------------------------------------------
| Action Creators
|--------------------------------------------------------------------------
*/
export function getCourses (page, limit) {
  return {
    types: [LOAD_COURSES_REQUEST, LOAD_COURSES_SUCCESS, LOAD_COURSES_FAILURE],
    callAPI: () => CoursesRepository.getCourses(page, limit)    
  }
}

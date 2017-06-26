import axios from 'axios'

import * as helpers from '../helpers/ReduxHelpers'


/*
 |--------------------------------------------------------------------------
 | Types
 |--------------------------------------------------------------------------
 */
export const LOAD_HOMEPAGE_REQUEST = 'LOAD_HOMEPAGE_REQUEST'
export const LOAD_HOMEPAGE_SUCCESS = 'LOAD_HOMEPAGE_SUCCESS'
export const LOAD_HOMEPAGE_FAILURE = 'LOAD_HOMEPAGE_FAILURE'

/*
 |--------------------------------------------------------------------------
 | Reducer
 |--------------------------------------------------------------------------
 */
const initialState = {
    hasData: false,
    requests: {}
}

export default helpers.createReducer(initialState, {
    [LOAD_HOMEPAGE_SUCCESS]: (state, action) => {
        return {
            hasData: true,
            requests: action.response
        }
    },
    [LOAD_HOMEPAGE_FAILURE]: (state, action) => {
        console.log('LOAD_HOMEPAGE_FAILURE')
        return initialState
    }
})

/*
|--------------------------------------------------------------------------
| Action Creators
|--------------------------------------------------------------------------
*/

/**
 * Reset user session status
 */
export function getHomepage() {
    return {
        types: [LOAD_HOMEPAGE_REQUEST, LOAD_HOMEPAGE_SUCCESS, LOAD_HOMEPAGE_FAILURE],
        callAPI: () => axios.get().then(resp => resp.data),
        shouldCallAPI: state => !state.hasData
    }
}


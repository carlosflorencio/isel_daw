import axios from '../data/axiosConfig'

import * as helpers from '../helpers/ReduxHelpers'
import ls from '../helpers/LocalStorage'

const HOMEPAGE_DATA_KEY = 'daw-homepage-resources'

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
  isFetching: true,
  requests: null
}

export default helpers.createReducer(initialState, {
  [LOAD_HOMEPAGE_REQUEST]: (state, action) => {
    return {
      ...state,
      isFetching: true,
    }
  },
  [LOAD_HOMEPAGE_SUCCESS]: (state, action) => {
    return {
      ...state,
      isFetching: false,
      requests: action.response
    }
  },
  [LOAD_HOMEPAGE_FAILURE]: (state, action) => {
    return {
      ...state,
      isFetching: false,
      requests: null
    }
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
  const cached = ls.get(HOMEPAGE_DATA_KEY)

  if (cached !== null) {
    // we have still valid cached data, dispatch success action
    return dispatch => {
      dispatch({ type: LOAD_HOMEPAGE_REQUEST })

      dispatch({ type: LOAD_HOMEPAGE_SUCCESS, response: cached })
    }
  }

  // no cached data, ask api again
  return {
    types: [
      LOAD_HOMEPAGE_REQUEST,
      LOAD_HOMEPAGE_SUCCESS,
      LOAD_HOMEPAGE_FAILURE
    ],
    callAPI: () =>
      axios().then(resp => resp.data).then(homepageData => {
        ls.set(HOMEPAGE_DATA_KEY, homepageData, 1000 * 60 * 60) // cache for 1h
        return homepageData
      }),
    shouldCallAPI: state => state.requests !== null
  }
}

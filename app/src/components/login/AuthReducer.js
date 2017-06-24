import * as helpers from '../../helpers/ReduxHelpers'
import User from '../../models/User'
import * as roles from '../../models/Roles'

/*
 |--------------------------------------------------------------------------
 | Types
 |--------------------------------------------------------------------------
 */
export const LOGOUT_SUCCESS = 'daw/auth/LOGOUT_SUCCESS'
export const LOGIN_SUCCESS = 'daw/auth/LOGIN_SUCCESS'

/*
 |--------------------------------------------------------------------------
 | Reducer
 |--------------------------------------------------------------------------
 */
const initialState = {
  isAuthenticated: localStorage['jwt'] !== undefined,
  user: localStorage['jwt'] !== undefined ? new User(1456, 'John Smith', roles.ADMIN) : null,
  jwt: null
}

export default helpers.createReducer(initialState, {
  [LOGOUT_SUCCESS]: (state, action) => {
    localStorage.removeItem('jwt')
    return {
      isAuthenticated: false,
      user: null,
      jwt: null
    }
  },
  [LOGIN_SUCCESS]: (state, action) => {
    //TODO: to be changed after oidc
    localStorage.setItem('jwt', btoa('pfelix@gmail.com:123456'))
    return {
      ...state,
      isAuthenticated: true,
      user: new User(1456, 'John Smith', roles.ADMIN)
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
export function logout () {
  return {
    type: LOGOUT_SUCCESS
  }
}

/**
 * Reset user session status
 */
export function login (user) {
  return {
    type: LOGIN_SUCCESS,
    email: user.email,
    password: user.password
  }
}

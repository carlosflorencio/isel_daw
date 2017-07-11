import * as reduxHelpers from '../../helpers/ReduxHelpers'
import * as oidcHelpers from '../../helpers/OIDCHelpers'
import User from '../../models/User'

/*
 |--------------------------------------------------------------------------
 | Types
 |--------------------------------------------------------------------------
 */
export const LOGIN_REQUEST = '´daw/auth/LOGIN_REQUEST'
export const LOGIN_SUCCESS = 'daw/auth/LOGIN_SUCCESS'
export const LOGIN_FAILURE = 'daw/auth/LOGIN_FAILURE'

export const LOGOUT_REQUEST = '´daw/auth/LOGOUT_REQUEST'
export const LOGOUT_SUCCESS = 'daw/auth/LOGOUT_SUCCESS'
export const LOGOUT_FAILURE = 'daw/auth/LOGOUT_FAILURE'

/*
 |--------------------------------------------------------------------------
 | Reducer
 |--------------------------------------------------------------------------
 */
const initialState = {
  isSigningIn: false,
  isAuthenticated: false,
  user: null,
  jwt: null
}

function logoutState(state, action) {
  return {
    ...state,
    isSigningIn: false,
    isAuthenticated: false,
    user: null,
    jwt: null
  }
}

export default reduxHelpers.createReducer(initialState, {
  [LOGIN_REQUEST]: (state, action) => {
    return {
      ...state,
      isSigningIn: true
    }
  },
  [LOGIN_SUCCESS]: (state, action) => {
    return {
      ...state,
      isSigningIn: false,
      isAuthenticated: true,
      user: new User(action.response.profile),
      jwt: action.response
    }
  },
  [LOGIN_FAILURE]: logoutState,
  [LOGOUT_SUCCESS]: logoutState
})

/*
 |--------------------------------------------------------------------------
 | Action Creators
 |--------------------------------------------------------------------------
 */

/**
 * Launches the login popup if not logged
 */
export function requestLogin() {
  const userManager = oidcHelpers.createUserManager()
  return {
    types: [LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE],
    callAPI: () => userManager.getUser().then(user => {
      if (user) {
        console.log('user! no popup needed', user)
        return user
      }
      console.log('no user! launching popup')

      return userManager.signinPopup().then(user => {
        console.log('Popup done!', user)
        if (user) {
          return user
        }

        throw new Error('user not logged')
      })
    })
  }
}

/**
 * Updates redux store info if the user is logged in
 */
export function updateLoginStatus() {
  const userManager = oidcHelpers.createUserManager()

  return {
    types: [LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE],
    callAPI: () => userManager.getUser().then(user => {
      if (user) {
        return user
      }
      throw new Error('user not logged')
    })
  }
}

/**
 * Launch logout popup
 */
export function requestLogout() {
  const userManager = oidcHelpers.createUserManager()

  return {
    types: [LOGOUT_REQUEST, LOGOUT_SUCCESS, LOGOUT_FAILURE],
    callAPI: () => userManager.getUser().then(user => {
      if(!user)
        return;

      return userManager.signoutPopup().then(_ => true)
    })
  }
}


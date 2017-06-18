import * as helpers from '../../helpers/ReduxHelpers'
import User from '../../models/User'
import * as roles from '../../models/Roles'

/*
 |--------------------------------------------------------------------------
 | Types
 |--------------------------------------------------------------------------
 */

/*
 |--------------------------------------------------------------------------
 | Reducer
 |--------------------------------------------------------------------------
 */
const initialState = {
  isAuthenticated: true,
  user: new User(1, "Nuno Reis", roles.ADMIN),
  jwt: null
}

export default helpers.createReducer(initialState, {})

/*
|--------------------------------------------------------------------------
| Action Creators
|--------------------------------------------------------------------------
*/

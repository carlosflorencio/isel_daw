import * as roles from './Roles'
/**
 * User Domain Model
 */
class User {
  constructor (name, role) {
    this.name = name
    this.role = role // array
  }

  hasRole (role) {
    if (this.role === role) {
      // direct role
      return true
    }

    // role does not exist! something is wrong
    if (!roles.defaultRoles[this.role]) return false

    // not a direct role? lets search in the child roles
    return roles.defaultRoles[this.role].includes(role)
  }
}

export default User

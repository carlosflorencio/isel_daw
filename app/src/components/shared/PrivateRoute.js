import React from 'react'
import { Redirect, Route } from 'react-router-dom'
import PropTypes from 'prop-types'

const PrivateRoute = ({ component: Component, session, minRole, ...rest }) => {
  console.log(session)
  let redirectPath = '/login'
  let hasPermission = false

  if (session.isAuthenticated) {
    if (session.user.hasRole(minRole)) {
      hasPermission = true
    } else {
      redirectPath = '/unauthorized'
    }
  }

  return (
    <Route
      {...rest}
      render={props =>
        hasPermission
          ? <Component {...props} />
          : <Redirect
            to={{ pathname: redirectPath, state: { from: props.location } }}
            />}
    />
  )
}

PrivateRoute.propTypes = {
  minRole: PropTypes.string.isRequired,
  component: PropTypes.any.isRequired
}

export default PrivateRoute
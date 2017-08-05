import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Menu, Button } from 'semantic-ui-react'
import { Link, NavLink } from 'react-router-dom'

class Navbar extends Component {
  constructor(props) {
    super(props)
    this.state = {}
  }

  render() {
    const { session, actions } = this.props
    return (
      <Menu className="no-border-radius" inverted>
        <Menu.Item>
          <Link to="/">Daw App</Link>
        </Menu.Item>
        <Menu.Item as={NavLink} to="/courses" content='Courses' />
        {session.isAuthenticated &&
          <Menu.Menu position="right">
            <Menu.Item name="user" content={session.user.name} />
            <Menu.Item
              as={Button}
              onClick={actions.requestLogout}
              name="logout"
              content="Logout"
            />
          </Menu.Menu>}
        {!session.isAuthenticated &&
          <Menu.Menu position="right">
            <Menu.Item
              as={Button}
              onClick={actions.requestLogin}
              to="/login"
              content={'Login'}
              name="login"
            />
          </Menu.Menu>}
      </Menu>
    )
  }
}

Navbar.propTypes = {
  session: PropTypes.object.isRequired,
  actions: PropTypes.object.isRequired
}

export default Navbar

/* global alert */
import React, { Component } from 'react'
import PropTypes from 'prop-types'
import logo from '../../assets/img/logo.svg'

import { Menu, Button } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

class Navbar extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }

        this.onClick = this.onClick.bind(this)
        this.onLogout = this.onLogout.bind(this)
    }

    onClick() {
        alert('Not Implemented')
    }

    onLogout() {
        this.props.actions.logout()
    }

    render() {
        const { session, actions } = this.props
        return (
            <Menu className="no-border-radius" inverted fixed='top'>
                <Menu.Item>
                    <img src={logo} alt='' />
                </Menu.Item>
                <Menu.Item 
                    as={NavLink}
                    to='/courses'
                    content={'Courses'}
                />
                {
                    session.isAuthenticated &&
                    <Menu.Menu position='right'>
                        <Menu.Item
                            name="user"
                            content={session.user.name}
                        />
                        <Menu.Item
                            as={Button}
                            onClick={actions.requestLogout}
                            name="logout"
                            content='Logout'
                            onClick={this.onLogout} />
                    </Menu.Menu>
                }
                {
                    !session.isAuthenticated &&
                    <Menu.Menu position='right'>
                        <Menu.Item
                            as={Button}
                            onClick={actions.requestLogin}
                            to='/login'
                            content={'Login'}
                            name="login" />
                    </Menu.Menu>
                }
            </Menu>
        )
    }
}

Navbar.propTypes = {
    session: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired
}

export default Navbar

/* global alert */
import React, { Component } from 'react'
import PropTypes from 'prop-types'
import logo from '../../assets/img/logo.svg'

import { Menu, Button } from 'semantic-ui-react'

class Navbar extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }

        this.onClick = this.onClick.bind(this)
    }

    onClick() {
        alert('Not Implemented')
    }

    render() {
        const { session } = this.props
        return (
            <Menu className="no-border-radius" inverted>
                <Menu.Item>
                    <img src={logo} alt=''/>
                </Menu.Item>
                <Menu.Menu position='right'>
                    <Menu.Item
                        as={Button}
                        name="user"
                        content={session.user.name}
                        onClick={this.onClick} />
                    <Menu.Item
                        as={Button}
                        name="logout"
                        content='Logout'
                        onClick={this.onClick} />
                </Menu.Menu>
            </Menu>
        )
    }
}

Navbar.propTypes = {
    session: PropTypes.object.isRequired
}

export default Navbar

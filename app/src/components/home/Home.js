import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Redirect } from 'react-router'

import { GUEST, STUDENT } from '../../models/Roles'

class Home extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { session } = this.props
        if(!session.isAuthenticated || session.user.hasRole(GUEST)){
            return (<Redirect to="courses" />)
        }
        return (
            <Redirect to={ 
                session.user.hasRole(STUDENT) ?
                    "students/"+session.user.id :
                    "/teachers/"+session.user.id
                }
            />
        )
    }
}

Home.propTypes = {
    session: PropTypes.object.isRequired
}

export default Home

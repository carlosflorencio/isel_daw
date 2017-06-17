import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Redirect } from 'react-router'

class Home extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { session } = this.props
        const role = session.user.role
        const id = session.user.id
        if(!session.isAuthenticated || role === "GUEST"){
            return (<Redirect to="courses" />)
        }
        return (
            <Redirect to={ role==="STUDENT" ? "students/"+id : "/teachers/"+id} />
        )
    }
}

Home.propTypes = {
    session: PropTypes.object.isRequired
}

export default Home

import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { TEACHER } from '../../models/Roles'

class ClassGroups extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { session } = this.props
        const {id} = this.props.match.params
        return (
            <div>
                <h1>Groups of Class {id}</h1>
                <h2>List of groups with option of deletion</h2>
                {
                    session.user.hasRole(TEACHER) &&
                    (<GroupForm />)
                }
            </div>
        )
    }
}

const GroupForm = () => {
    return (<h1>Add Group To Class. Only For Teacher or Higher</h1>)
}

export default ClassGroups

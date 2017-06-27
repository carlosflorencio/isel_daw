import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { TEACHER } from '../../models/Roles'

class ClassStudents extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        // const { session } = this.props
        return (
            <div>
                <h1>List of Students relative to a Class</h1>
                <h2>(Link to Group of Student)</h2>
                <h2>(Link to Student Details)</h2>
                {/*{
                    session.user.hasRole(TEACHER) && 
                    <h1>Form to add a student (Only for teacher and above)</h1>
                }*/}
            </div>
        )
    }
}

ClassStudents.propTypes = {
    session: PropTypes.object.isRequired
}

export default ClassStudents

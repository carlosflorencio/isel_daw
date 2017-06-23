import React, { Component } from 'react'

import { Button } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import { ADMIN } from '../../models/Roles'

class Class extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { session } = this.props
        const { id } = this.props.match.params
        return (
            <div>
                <h1>Class {id}</h1>
                <h2>Teachers of the Class and respective Link</h2>
                {
                    session.user.hasRole(ADMIN) &&  // Coordinator of the course
                    (<TeacherForm />)
                }
                <Button as={NavLink} to={"/classes/" + id + "/groups"}>
                    Groups of the Class
                </Button>
                <Button as={NavLink} to={"/classes/" + id + "/students"}>
                    Students of the Class
                </Button>
            </div>
        )
    }
}

const TeacherForm = () => (
 <h1>Teacher Form Only for the Coordinator of the Course Class</h1>
)

export default Class

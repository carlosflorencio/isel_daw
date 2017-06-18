import React, { Component } from 'react'

import {Button} from 'semantic-ui-react'
import {NavLink} from 'react-router-dom'

import { ADMIN } from '../../models/Roles'

class Course extends Component {
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
                <h1>Course {id}</h1>
                <h2>The course details</h2>
                <Button as={NavLink} to={"/courses/"+id+"/classes"}>
                    Course Classes
                </Button>
                {
                    session.user.hasRole(ADMIN) &&
                    (<ClassForm />)
                }
            </div>
        )
    }
}

const ClassForm = () => (
 <h1>Class Form Only for Admins</h1>
);

export default Course

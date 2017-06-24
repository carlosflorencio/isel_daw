import React, { Component } from 'react'
import PropTypes from 'prop-types'

import {Button} from 'semantic-ui-react'
import {NavLink} from 'react-router-dom'

import CoursesRepository from '../../data/repositories/CoursesRepository'

import { ADMIN } from '../../models/Roles'

class Course extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        CoursesRepository.getCourse(this.props.match.params.id)
            .then(course => console.log(course))
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
                    session.isAuthenticated &&
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

Course.propTypes = {
    session: PropTypes.object.isRequired
}

export default Course

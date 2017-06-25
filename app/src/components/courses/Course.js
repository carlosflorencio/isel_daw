import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Segment } from 'semantic-ui-react'

import ClassesRepository from '../../data/repositories/ClassesRepository'
import CoursesRepository from '../../data/repositories/CoursesRepository'

import CourseClasses from '../classes/CourseClasses'

import { ADMIN, STUDENT } from '../../models/Roles'

class Course extends Component {
    constructor(props) {
        super(props)
        let params = new URLSearchParams(props.location.search)
        this.state = {
            isLoading: true,
            page: params.page || 1,
            limit: params.limit || 5
        }
    }

    componentDidMount() {
        CoursesRepository.getCourse(this.props.match.params.id)
            .then(course => {
                console.log(course)
                this.setState({ course, isLoading: false })
            }).then(_ =>
                ClassesRepository.getCourseClasses(
                    this.props.match.params.id,
                    this.state.page,
                    this.state.limit
                ).then(classes => {
                    console.log(classes)
                    this.setState({ classes })
                })
            )
    }

    render() {
        const { session } = this.props
        const { course, classes, isLoading } = this.state
        return (
            <Segment basic textAlign='center' loading={isLoading}>
                {
                    course &&
                    <h1>{course.properties['name']} ({course.properties['acr']})</h1>
                }
                {
                    session.isAuthenticated &&
                    session.user.hasRole(STUDENT) &&
                    classes &&
                    <CourseClasses classes={classes} />
                }
                {
                    session.isAuthenticated &&
                    session.user.hasRole(ADMIN) &&
                    (<ClassForm />)
                }
            </Segment>
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

import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment } from 'semantic-ui-react'

import SirenHelpers from '../../helpers/SirenHelpers'
import CourseClasses from '../classes/CourseClasses'
import CustomForm from '../shared/CustomForm'

import {CourseEntry} from '../../data/ApiContracts'

import { STUDENT } from '../../models/Roles'

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
        let uri = this.props.api.requests[CourseEntry]
            .replace('{id}', this.props.match.params.id)
        axios.get(uri).then(resp => resp.data)
            .then(course => {
                console.log(course)
                this.setState({ course, isLoading: false })
                return SirenHelpers.getLink(course, '/relations#course-classes')
            })
            .then(href => axios.get(
                href, 
                { params: { page: this.state.page, limit: this.state.limit } })
            ).then(resp => resp.data)
            .then(classes => {
                console.log(classes)
                this.setState({ classes })
            })
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
                    course &&
                    course.actions &&
                    (<CustomForm action={SirenHelpers.getAction(course, 'add-class-to-course')} />)
                }
            </Segment>
        )
    }
}

Course.propTypes = {
    session: PropTypes.object.isRequired
}

export default Course

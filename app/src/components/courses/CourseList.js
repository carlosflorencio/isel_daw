import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { ADMIN } from '../../models/Roles'

import CoursesRepository from '../../data/repositories/CoursesRepository'

class CourseList extends Component {
    constructor(props) {
        super(props)
        let params = new URLSearchParams(props.location.search);
        this.state = {
            page: params.page || 1,
            limit: params.limit || 5
        }
    }

    componentDidMount() {
        CoursesRepository.getCourses(this.state.page, this.state.limit)
            .then(courses => console.log(courses))
    }

    render() {
        const { session } = this.props
        return (
            <div>
                <h1>Paginated List of Courses</h1>
                {
                    session.isAuthenticated &&
                    session.user.hasRole(ADMIN) &&  // Coordinator of the course
                    (<CourseForm />)
                }
            </div>
        )
    }
}

const CourseForm = () => (
 <h1>Course Form Only for the Coordinator of the Course</h1>
)

CourseList.propTypes = {
    session: PropTypes.object.isRequired
}

export default CourseList

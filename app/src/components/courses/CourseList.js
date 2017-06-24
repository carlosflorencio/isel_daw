import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { ADMIN } from '../../models/Roles'

class CourseList extends Component {
    constructor(props) {
        super(props)
        this.state = {
            page: 1,
            limit: 5
        }
    }

    componentDidMount() {
        this.props.actions.getCourses(this.state.page, this.state.limit)
            .then(_ => console.log(this.props.courses))
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
    session: PropTypes.object.isRequired,
    courses: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired
}

export default CourseList

import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { ADMIN } from '../../models/Roles'

class CourseList extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { session } = this.props
        return (
            <div>
                <h1>Paginated List of Courses</h1>
                {
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

import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Table, Segment } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import CoursesRepository from '../../data/repositories/CoursesRepository'

class CourseList extends Component {
    constructor(props) {
        super(props)
        let params = new URLSearchParams(props.location.search)
        this.state = {
            courses: {},
            page: params.page || 1,
            limit: params.limit || 5
        }
    }

    componentDidMount() {
        CoursesRepository.getCourses(this.state.page, this.state.limit)
            .then(courses => {
                console.log(courses)
                this.setState({ courses })
            })
    }

    render() {
        const { courses } = this.state
        const { session } = this.props
        return (
            <Segment className='padding-left-right' basic>
                <Table celled striped selectable color='teal'>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell colSpan='3' textAlign='center'>
                                Courses
                            </Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {
                            courses.entities &&
                            courses.entities.map(course => {
                                return (
                                    <Table.Row key={course.properties['id']}>
                                        <Table.Cell collapsing >
                                            {course.properties['acr']}
                                        </Table.Cell>
                                        <Table.Cell collapsing>
                                            {course.properties['name']}
                                        </Table.Cell>
                                        <Table.Cell collapsing>
                                            <NavLink
                                                to={'/courses/' + course.properties['id']}>
                                                Details
                                            </NavLink>
                                        </Table.Cell>
                                    </Table.Row>
                                )
                            })
                        }
                    </Table.Body>
                </Table>
                {
                    session.isAuthenticated &&
                    courses.actions &&
                    (<CourseForm action={courses.actions[0]} />)
                }
            </Segment>
        )
    }
}

const CourseForm = ({ action }) => (
    <h1>{action.title}</h1>

)

CourseList.propTypes = {
    session: PropTypes.object.isRequired
}

export default CourseList

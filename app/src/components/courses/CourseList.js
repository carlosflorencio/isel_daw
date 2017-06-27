import React, { Component } from 'react'
import PropTypes from 'prop-types'

import {Table} from 'semantic-ui-react'
import {NavLink} from 'react-router-dom'

class CourseList extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { header, courses } = this.props
        return (
            <Table celled striped selectable color='teal'>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell colSpan='3' textAlign='center'>
                                {header}
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
        )
    }
}

CourseList.propTypes = {
    header: PropTypes.string.isRequired,
    courses: PropTypes.object.isRequired
}

export default CourseList

import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Table } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

class TeachersList extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { teachers } = this.props
        return (
            <Table celled striped selectable color='teal'>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell colSpan='4' textAlign='center'>
                            Teachers
                            </Table.HeaderCell>
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {
                        teachers.entities &&
                        teachers.entities.map(teacher => {
                            return (
                                <Table.Row key={teacher.properties['number']}>
                                    <Table.Cell collapsing >
                                        {teacher.properties['name']}
                                    </Table.Cell>
                                    <Table.Cell collapsing>
                                        {teacher.properties['email']}
                                    </Table.Cell>
                                    <Table.Cell collapsing>
                                        <NavLink
                                            to={'/teachers/' + teacher.properties['number']}>
                                            Details
                                                </NavLink>
                                    </Table.Cell>
                                    {
                                        // Delete
                                        <Table.Cell collapsing>
                                            Delete
                                        </Table.Cell>
                                    }
                                </Table.Row>
                            )
                        })
                    }
                </Table.Body>
            </Table>
        )
    }
}

TeachersList.propTypes = {
    teachers: PropTypes.object.isRequired
}

export default TeachersList


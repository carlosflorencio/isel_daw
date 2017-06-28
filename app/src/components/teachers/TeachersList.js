import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Table, Button } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import EntityActionCell from '../shared/EntityActionCell'
import SirenHelpers from '../../helpers/SirenHelpers'

class TeachersList extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { teachers, actionRel } = this.props
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
                                    <EntityActionCell
                                        onClick={() => axios(
                                            SirenHelpers.createAxiosConfig(teacher, actionRel)
                                        )}
                                        action={SirenHelpers.getAction(teacher, actionRel)}
                                    />
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
    teachers: PropTypes.object.isRequired,
}

export default TeachersList


import React, { Component } from 'react'
import axios from 'axios'

import { Table } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import { ClassGroupsList } from '../../data/ApiContracts'

class ClassGroups extends Component {
    constructor(props) {
        super(props)
        this.state = {
            groups: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[ClassGroupsList]
            .replace('{id}', this.props.match.params.id)

        axios.get(uri)
            .then(resp => resp.data)
            .then(groups => {
                console.log(groups)
                this.setState({ groups })
            })
    }

    render() {
        return (
            <GroupsList
                classId={this.props.match.params.id}
                groups={this.state.groups}
            />
        )
    }
}

const GroupsList = ({ classId, groups }) => {
    return (
        <Table celled striped selectable color='teal'>
            <Table.Header>
                <Table.Row>
                    <Table.HeaderCell colSpan='4' textAlign='center'>
                        Groups
                </Table.HeaderCell>
                </Table.Row>
            </Table.Header>
            <Table.Body>
                {
                    groups.entities &&
                    groups.entities.map(group => {
                        return (
                            <Table.Row key={group.properties['number']}>
                                <Table.Cell collapsing >
                                    Group {group.properties['number']}
                                </Table.Cell>
                                <Table.Cell collapsing>
                                    <NavLink
                                        to={'/classes/' + classId + '/groups/' +
                                            group.properties['number']}>
                                        Details
                                        </NavLink>
                                </Table.Cell>
                                {
                                    // Remove Group
                                }
                            </Table.Row>
                        )
                    })
                }
            </Table.Body>
        </Table>
    )
}

export default ClassGroups

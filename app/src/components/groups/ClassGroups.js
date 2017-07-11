import React, { Component } from 'react'
import axios from '../../data/axiosConfig'

import { Segment, Table } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import { ClassGroupsList } from '../../data/ApiContracts'
import SirenHelpers from '../../helpers/SirenHelpers'
import CustomForm from '../shared/CustomForm'
import EntityActionCell from '../shared/EntityActionCell'

class ClassGroups extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading: true,
            groups: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[ClassGroupsList]
            .replace('{id}', this.props.match.params.id)

        axios(uri)
            .then(resp => resp.data)
            .then(groups => {
                console.log(groups)
                this.setState({ groups, isLoading:false })
            })
    }

    render() {
        const { groups, isLoading } = this.state
        return (
            <Segment basic loading={isLoading}>
                <GroupsList
                    classId={this.props.match.params.id}
                    groups={groups}
                    actionRel={'delete-group'}
                />
                {
                    groups.actions &&
                    <CustomForm 
                        action={SirenHelpers.getAction(
                            groups,
                            'add-group-to-class'
                        )}
                    />
                }
            </Segment>
        )
    }
}

const GroupsList = ({ classId, groups, actionRel }) => {
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
                                <EntityActionCell
                                        onClick={() => {
                                            let conf = SirenHelpers.createAxiosConfig(group, actionRel)
                                            axios(conf.url,conf.method)
                                        }}
                                        action={SirenHelpers.getAction(group, actionRel)}
                                    />
                            </Table.Row>
                        )
                    })
                }
            </Table.Body>
        </Table>
    )
}

export default ClassGroups

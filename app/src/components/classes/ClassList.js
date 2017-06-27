import React, { Component } from 'react'
import PropTypes from 'prop-types'

import {Table, Segment} from 'semantic-ui-react'
import {NavLink} from 'react-router-dom'

class ClassList extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const {classes} = this.props
        return (
            <Segment className='padding-left-right' basic>
                <Table celled striped selectable color='teal'>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell colSpan='2' textAlign='center'>
                                Classes
                            </Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {
                            classes.entities &&
                            classes.entities.map(c => {
                                return (
                                    <Table.Row key={c.properties['id']}>
                                        <Table.Cell collapsing>
                                            <b>{c.properties['name']}</b>
                                        </Table.Cell>
                                        <Table.Cell collapsing>
                                            <NavLink
                                                to={'/classes/' + c.properties['id']}>
                                                Details
                                            </NavLink>
                                        </Table.Cell>
                                    </Table.Row>
                                )
                            })
                        }
                    </Table.Body>
                </Table>
            </Segment>
        )
    }
}

ClassList.propTypes = {
    classes: PropTypes.object.isRequired
}

export default ClassList

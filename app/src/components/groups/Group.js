import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import { Segment, Card, Image } from 'semantic-ui-react'

import { GroupEntry } from '../../data/ApiContracts'

class Group extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading: true,
            group: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[GroupEntry]
            .replace('{id}', this.props.match.params.groupId)
        axios(uri)
            .then(resp => resp.data)
            .then(group => {
                console.log(group)
                this.setState({ group, isLoading: false })
            })
    }

    render() {
        const { group, isLoading } = this.state
        return (
            <Segment loading={isLoading}>
                {
                    group.properties &&
                    <h1>Group Number {group.properties['number']}</h1>
                }
                <Segment basic textAlign='center'>
                    <Card.Group itemsPerRow={3}>
                        {
                            group.entities &&
                            group.entities.map(item =>
                                <Card color='teal'>
                                    <Image src='http://via.placeholder.com/300x150' />
                                    <Card.Content>
                                        <Card.Header>{item.properties['name']}</Card.Header>
                                    </Card.Content>
                                </Card>
                            )
                        }
                    </Card.Group>
                </Segment>
            </Segment>
        )
    }
}

Group.propTypes = {
    api: PropTypes.object.isRequired
}

export default Group

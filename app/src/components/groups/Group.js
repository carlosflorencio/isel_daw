import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment } from 'semantic-ui-react'

import { GroupEntry } from '../../data/ApiContracts'

class Group extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading:true,
            group: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[GroupEntry]
            .replace('{id}', this.props.match.params.id)
        axios.get(uri)
            .then(resp => resp.data)
            .then(group => {
                console.log(group)
                this.setState({group, isLoading:false})
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
            </Segment>
        )
    }
}

Group.propTypes = {
    api: PropTypes.object.isRequired
}

export default Group

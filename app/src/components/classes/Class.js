import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment, Header } from 'semantic-ui-react'
import CustomForm from '../shared/CustomForm'

import SirenHelpers from '../../helpers/SirenHelpers'

import { ClassEntry } from '../../data/ApiContracts'

class Class extends Component {
    constructor(props) {
        super(props)
        this.state = {
            cl: {}
        }
    }

    componentDidMount() {
        var uri = this.props.api.requests[ClassEntry]
            .replace("{id}", this.props.match.params.id)

        axios.get(uri)
            .then(resp => resp.data)
            .then(cl => {
                console.log(cl)
                this.setState({ cl })
            })
    }

    render() {
        const { cl } = this.state
        return (
            <Segment color='teal' padded>
                {
                    cl.properties &&
                    <div>
                        <Header as='h1' textAlign='left'>
                            Class {cl.properties.name}
                        </Header>
                        <Header as='h2' textAlign='left'>
                            Max group size: {cl.properties.maxGroupSize}
                        </Header>
                        <Header as='h2' textAlign='left'>
                            Auto Enrollment: {
                                cl.properties.autoEnrollment ? 'Yes' : 'No'
                            }
                        </Header>
                    </div>
                }
                {
                    cl.actions &&
                    <Segment basic>
                        <CustomForm 
                            action={
                                SirenHelpers.getAction(cl, 'add-teacher-to-class')
                            }
                        />
                    </Segment>
                }
            </Segment>
        )
    }
}

Class.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default Class

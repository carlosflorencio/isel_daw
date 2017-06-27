import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment } from 'semantic-ui-react'
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
        const { cl, teachers } = this.state
        const { id } = this.props.match.params
        return (
            <Segment basic textAlign='center'>
                {
                    cl.properties &&
                    <div>
                        <h1>Class {cl.properties.name}</h1>
                        <h2>Max group size: {cl.properties.maxGroupSize}</h2>
                        <h2>
                            Auto Enrollment: {
                                cl.properties.autoEnrollment ? 'Yes' : 'No'
                            }
                        </h2>
                    </div>
                }
                {/*{
                    //TODO: Missing Remove Teacher from Class =S
                    teachers &&
                    <TeachersList
                        teachers={teachers}
                    />
                }*/}
                {
                    cl.actions &&
                    (<CustomForm 
                        action={
                            SirenHelpers.getAction(cl, 'add-teacher-to-class')
                        }
                    />)
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

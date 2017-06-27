import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment, Button } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import SirenHelpers from '../../helpers/SirenHelpers'

import { ClassEntry } from '../../data/ApiContracts'
import CustomForm from '../shared/CustomForm'
import TeachersList from '../teachers/TeachersList'

class Class extends Component {
    constructor(props) {
        super(props)
        this.state = {
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
                return SirenHelpers.getLink(cl, "/relations/class#teachers")
            })
            .then(href => axios.get(href))
            .then(resp => resp.data)
            .then(teachers => {
                console.log(teachers)
                this.setState({ teachers })
            })
    }

    render() {
        const { cl, teachers } = this.state
        const { id } = this.props.match.params
        return (
            <Segment basic textAlign='center'>
                {
                    cl &&
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
                {
                    teachers &&
                    <TeachersList teachers={teachers}/>
                }
                {
                    cl &&
                    cl.actions &&
                    (<CustomForm 
                        action={
                            SirenHelpers.getAction(cl, 'add-teacher-to-class')
                        }
                    />)
                }
                <Button as={NavLink} to={"/classes/" + id + "/groups"}>
                    Groups of the Class
                </Button>
                <Button as={NavLink} to={"/classes/" + id + "/students"}>
                    Students of the Class
                </Button>
            </Segment>
        )
    }
}

Class.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default Class

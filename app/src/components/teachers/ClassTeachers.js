import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import {Segment} from 'semantic-ui-react'

import SirenHelpers from '../../helpers/SirenHelpers'
import { ClassTeachersList } from '../../data/ApiContracts'

import CustomForm from '../shared/CustomForm'
import TeachersList from './TeachersList'

class ClassTeachers extends Component {
    constructor(props) {
        super(props)
        this.state = {
            teachers: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[ClassTeachersList]
            .replace('{id}', this.props.match.params.id)

        axios.get(uri)
            .then(resp => resp.data)
            .then(teachers => {
                console.log(teachers)
                this.setState({ teachers })
            })
    }

    render() {
        const {teachers} = this.state
        return (
            <Segment basic>
                <TeachersList 
                    teachers={teachers}
                    actionRel={'remove-teacher-from-class'}
                />
                {
                    teachers.actions &&
                    <CustomForm 
                    action={SirenHelpers.getAction(
                        teachers,
                        'add-teacher-to-class'
                    )}
                />
                }
            </Segment>
        )
    }
}

ClassTeachers.propTypes = {
    api: PropTypes.object.isRequired
}

export default ClassTeachers

import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import {Segment} from 'semantic-ui-react'

import SirenHelpers from '../../helpers/SirenHelpers'
import {
    ClassTeachersList,
    REMOVE_CLASS_TEACHER,
    ADD_CLASS_TEACHER
} from '../../data/ApiContracts'

import CustomForm from '../shared/CustomForm'
import TeachersList from './TeachersList'

class ClassTeachers extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading: true,
            teachers: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[ClassTeachersList]
            .replace('{id}', this.props.match.params.id)

        axios(uri)
            .then(resp => resp.data)
            .then(teachers => {
                console.log(teachers)
                this.setState({ teachers, isLoading:false })
            })
    }

    render() {
        const {teachers, isLoading} = this.state
        return (
            <Segment basic loading={isLoading}>
                <TeachersList 
                    teachers={teachers}
                    actionRel={REMOVE_CLASS_TEACHER}
                />
                {
                    teachers.actions &&
                    <CustomForm 
                    action={SirenHelpers.getAction(
                        teachers,
                        ADD_CLASS_TEACHER
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

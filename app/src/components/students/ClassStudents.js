import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import SirenHelpers from '../../helpers/SirenHelpers'

import { Segment } from 'semantic-ui-react'

import { ClassStudentsList } from '../../data/ApiContracts'
import StudentsList from './StudentsList'
import CustomForm from '../shared/CustomForm'

class ClassStudents extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isLoading: true,
            students: {}
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[ClassStudentsList]
            .replace('{id}', this.props.match.params.id)

        axios(uri)
            .then(resp => resp.data)
            .then(students => {
                console.log(students)
                this.setState({ students, isLoading: false })
            })
    }

    render() {
        const { students, isLoading } = this.state
        return (
            <Segment basic loading={isLoading}>
                <StudentsList
                    students={students}
                    actionRel={'remove-student-from-class'}
                />
                {
                    students.actions &&
                    <CustomForm
                        action={SirenHelpers.getAction(
                            students,
                            'add-student-to-class'
                        )}
                    />
                }
            </Segment>
        )
    }
}

ClassStudents.propTypes = {
    api: PropTypes.object.isRequired
}

export default ClassStudents

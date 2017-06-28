import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import SirenHelpers from '../../helpers/SirenHelpers'

import {Segment} from 'semantic-ui-react'

import { ClassStudentsList } from '../../data/ApiContracts'
import StudentsList from './StudentsList'
import CustomForm from '../shared/CustomForm'

class ClassStudents extends Component {
    constructor(props) {
        super(props)
        this.state = {
            students: {}
        }
    }
    
    componentDidMount() {
        let uri = this.props.api.requests[ClassStudentsList]
            .replace('{id}', this.props.match.params.id)

        axios.get(uri)  
            .then(resp => resp.data)
            .then(students => {
                console.log(students)
                this.setState({ students })
            })
    }

    render() {
        const { students } = this.state
        return (
            <Segment basic>
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

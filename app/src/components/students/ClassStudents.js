import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { ClassStudentsList } from '../../data/ApiContracts'
import StudentsList from './StudentsList'

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
        return (
            <StudentsList students={this.state.students} />
        )
    }
}

ClassStudents.propTypes = {
    api: PropTypes.object.isRequired
}

export default ClassStudents

import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { ClassTeachersList } from '../../data/ApiContracts'

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
        return (
            <TeachersList teachers={this.state.teachers}/>
        )
    }
}

ClassTeachers.propTypes = {
    api: PropTypes.object.isRequired
}

export default ClassTeachers

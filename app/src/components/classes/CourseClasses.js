import React, { Component } from 'react'
import PropTypes from 'prop-types'

import ClassesRepository from '../../data/repositories/ClassesRepository'

class CourseClasses extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        ClassesRepository.getCourseClasses(this.props.match.params.id)
            .then(classes => console.log(classes))
    }

    render() {
        const { id } = this.props.match.params
        return (
            <div>
                <h1>Classes of Course {id}</h1>
                <h2>List of classes with link to details</h2>
            </div>
        )
    }
}

export default CourseClasses

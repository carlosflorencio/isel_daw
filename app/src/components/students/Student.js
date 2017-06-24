import React, { Component } from 'react'

import StudentsRepository from '../../data/repositories/StudentsRepository'

class Student extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        StudentsRepository.getStudent(this.props.match.params.id)
            .then(student => console.log(student))
    }

    render() {
        return (
            <div>
                <h1>Student</h1>
            </div>
        )
    }
}

export default Student

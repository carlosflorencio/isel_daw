import React, { Component } from 'react'

import TeacherRepository from '../../data/repositories/TeacherRepository'

class Teacher extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        TeacherRepository.getTeacher(this.props.match.params.id)
            .then(teacher => console.log(teacher))
            .catch(error => console.log(error.message))
    }

    render() {
        return (
            <div>
                <h1>Teacher</h1>
            </div>
        )
    }
}

export default Teacher

import React, { Component } from 'react'

import axios from 'axios'

class Teacher extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        // TeacherRepository.getTeacher(this.props.match.params.id)
        //     .then(teacher => console.log(teacher))
        //     .catch(error => console.log(error.message))
        axios.get('http://localhost:5000/api/teachers/'+this.props.match.params.id)
            .then(resp => resp.data)
            .then(teacher => console.log(teacher))
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

import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment, Card, Image } from 'semantic-ui-react'

import { TeacherEntry } from '../../data/ApiContracts'

import SirenHelpers from '../../helpers/SirenHelpers'

import ClassList from '../classes/ClassList'
import CourseList from '../courses/CourseList'

class Teacher extends Component {
    constructor(props) {
        super(props)
        this.state = {
            teacher: {},
            classes: {},
            courses: {}
        }
    }

    componentDidMount() {
        const uri = this.props.api.requests[TeacherEntry]
            .replace("{number}", this.props.match.params.id)

        axios.get(uri).then(resp => resp.data)
            .then(teacher => {
                console.log(teacher)
                this.setState({ teacher })
                return [
                    SirenHelpers.getLink(teacher, '/relations#teacher-classes'),
                    SirenHelpers.getLink(teacher, '/relations#teacher-courses')
                ]
            })
            .then(hrefs => {
                axios.get(hrefs[0]).then(resp => resp.data)
                    .then(classes => this.setState({classes}))
                axios.get(hrefs[1]).then(resp => resp.data)
                    .then(courses => this.setState({courses}))
            })
    }

    render() {
        const { teacher, classes, courses } = this.state
        return (
            <Segment basic textAlign='center'>
                <Segment className='padding-left-right' color='teal'>
                    <h1>Teacher</h1>
                    {
                        teacher.properties &&
                        <Card centered>
                            <Image src='http://via.placeholder.com/300x150' />
                            <Card.Content>
                                <Card.Header>
                                    {teacher.properties['name']}
                                </Card.Header>
                                <Card.Description>
                                    Email: {teacher.properties['email']}
                                </Card.Description>
                            </Card.Content>
                        </Card>
                    }
                    {
                        classes.entities &&
                        <ClassList header={'Classes you teach'} classes={classes} />
                    }
                    {
                        courses.entities &&
                        <CourseList header={'Courses Coordinated by you'} courses={courses} />
                    }
                </Segment>
            </Segment>
        )
    }
}

Teacher.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default Teacher

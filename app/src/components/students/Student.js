import React, { Component } from 'react'

import { Segment, Card, Image } from 'semantic-ui-react'

import StudentsRepository from '../../data/repositories/StudentsRepository'

class Student extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        StudentsRepository.getStudent(this.props.match.params.id)
            .then(student => {
                console.log(student)
                this.setState({student})
            })
    }

    render() {
        const {student} = this.state
        return (
            <Segment basic textAlign='center'>
                <Segment className='padding-left-right' color='teal'>
                    <h1>Student</h1>
                    {
                        student &&
                        <Card centered>
                            <Image src='http://via.placeholder.com/300x150' />
                            <Card.Content>
                                <Card.Header>
                                    {student.properties['name']}
                                </Card.Header>
                                <Card.Description>
                                    Email: {student.properties['email']}
                                </Card.Description>
                            </Card.Content>
                        </Card>
                    }
                    {
                        //teacher &&
                        //<DeleteTeacher 
                        //    action={SirenHelpers.getAction(teacher, 'delete-teacher')}
                        ///>
                    }
                </Segment>
            </Segment>
        )
    }
}

export default Student

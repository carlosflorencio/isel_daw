import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment, Card, Image } from 'semantic-ui-react'

import { StudentEntry } from '../../data/ApiContracts'

class Student extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        const uri = this.props.api.requests[StudentEntry]
            .replace("{number}", this.props.match.params.id)
        axios.get(uri).then(resp =>  resp.data)
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

Student.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default Student

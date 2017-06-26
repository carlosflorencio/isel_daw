import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Segment, Card, Image } from 'semantic-ui-react'

import { TeacherEntry } from '../../data/ApiContracts'

//import SirenHelpers from '../../helpers/SirenHelpers'

class Teacher extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        const uri = this.props.api.requests[TeacherEntry]
                .replace("{number}", this.props.match.params.id)

        axios.get(uri).then(resp => resp.data)
            .then(teacher => {
                console.log(teacher)
                this.setState({ teacher })
            })
    }

    render() {
        const { teacher } = this.state
        return (
            <Segment basic textAlign='center'>
                <Segment className='padding-left-right' color='teal'>
                    <h1>Teacher</h1>
                    {
                        teacher &&
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

Teacher.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default Teacher

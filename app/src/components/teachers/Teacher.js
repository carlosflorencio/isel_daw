import React, { Component } from 'react'
import axios from 'axios'

import { Segment, Card, Image } from 'semantic-ui-react'

//import SirenHelpers from '../../helpers/SirenHelpers'

class Teacher extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        // First GET can be requested once in the begining and stored in redux store
        axios.get()
            .then(resp => resp.data)
            .then(home => axios.get(
                home.TeacherEntry.replace("{number}", this.props.match.params.id))
            )
            .then(resp => resp.data)
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

export default Teacher

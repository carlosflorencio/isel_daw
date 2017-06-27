import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

import { Table, Segment } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import CustomForm from '../shared/CustomForm'

import {CourseList as CourseListRequest} from '../../data/ApiContracts'

class CourseList extends Component {
    constructor(props) {
        super(props)
        let params = new URLSearchParams(props.location.search)
        this.state = {
            isLoading:true,
            courses: {},
            page: params.page || 1,
            limit: params.limit || 5
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[CourseListRequest]
        axios.get(uri).then(resp => resp.data)
            .then(courses => {
                console.log(courses)
                this.setState({ courses, isLoading:false })
            })
    }

    render() {
        const { courses, isLoading } = this.state
        return (
            <Segment basic className='padding-left-right' loading={isLoading}>
                <Table celled striped selectable color='teal'>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell colSpan='3' textAlign='center'>
                                Courses
                            </Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {
                            courses.entities &&
                            courses.entities.map(course => {
                                return (
                                    <Table.Row key={course.properties['id']}>
                                        <Table.Cell collapsing >
                                            {course.properties['acr']}
                                        </Table.Cell>
                                        <Table.Cell collapsing>
                                            {course.properties['name']}
                                        </Table.Cell>
                                        <Table.Cell collapsing>
                                            <NavLink
                                                to={'/courses/' + course.properties['id']}>
                                                Details
                                            </NavLink>
                                        </Table.Cell>
                                    </Table.Row>
                                )
                            })
                        }
                    </Table.Body>
                </Table>
                {
                    courses.actions &&
                    (<CustomForm action={courses.actions[0]} />)
                }
            </Segment>
        )
    }
}

CourseList.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default CourseList

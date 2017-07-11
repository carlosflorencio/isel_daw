import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import { Segment } from 'semantic-ui-react'

import CustomForm from '../shared/CustomForm'
import CourseList from './CourseList'

import {CourseList as CourseListRequest} from '../../data/ApiContracts'

class CoursesPage extends Component {
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

    componentDidUpdate(prevProps){
        if(this.props.session.isAuthenticated ^ prevProps.session.isAuthenticated){
            let uri = this.props.api.requests[CourseListRequest]
            axios(uri).then(resp => resp.data)
                .then(courses => {
                    console.log(courses)
                    this.setState({ courses, isLoading:false })
                })
        }
    }

    componentDidMount() {
        let uri = this.props.api.requests[CourseListRequest]
        axios(uri).then(resp => resp.data)
            .then(courses => {
                console.log(courses)
                this.setState({ courses, isLoading:false })
            })
    }

    render() {
        const { courses, isLoading } = this.state
        return (
            <Segment basic className='padding-left-right' loading={isLoading}>
                {
                    courses.entities && 
                    <CourseList header={'Courses'} courses={courses} />
                }
                {
                    courses.actions &&
                    (<CustomForm action={courses.actions[0]} />)
                }
            </Segment>
        )
    }
}

CoursesPage.propTypes = {
    api: PropTypes.object.isRequired,
    session: PropTypes.object.isRequired
}

export default CoursesPage

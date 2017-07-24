import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import { Segment } from 'semantic-ui-react'

import CustomForm from '../shared/CustomForm'
import CourseList from './CourseList'

import { CourseList as CourseListRequest } from '../../data/ApiContracts'

class CoursesPage extends Component {
  constructor(props) {
    super(props)
    let params = new URLSearchParams(props.location.search)
    this.state = {
      isLoading: true,
      courses: {},
      params: {
        page: params.get('page') || 1,
        limit: params.get('limit') || 5
      }
    }

    this.getData = this.getData.bind(this)
  }

  componentWillReceiveProps(nextProps) {
    let nextParams = new URLSearchParams(nextProps.location.search)
    if (nextParams.get('page') !== this.state.params.page ||
      nextParams.get('limit') !== this.state.params.limit || 
      this.props.session.isAuthenticated ^ nextProps.session.isAuthenticated) {
      this.setState({
        isLoading: true,
        courses: {},
        params: {
          page: nextParams.get('page') || this.state.params.page,
          limit: nextParams.get('limit') || this.state.params.limit
        }
      })
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if (this.props.session.isAuthenticated ^ prevProps.session.isAuthenticated ||
        this.state.params !== prevState.params) {
      this.getData()
    }
  }

  componentDidMount() {
    this.getData()
  }

  getData() {
    let uri = this.props.api.requests[CourseListRequest]
    axios(uri, { params: this.state.params })
      .then(resp => resp.data)
      .then(courses => {
        console.log(courses)
        this.setState({ courses, isLoading: false })
      })
  }

  render() {
    const { courses, isLoading } = this.state
    return (
      <Segment basic className="padding-left-right" loading={isLoading}>
        {courses.entities &&
          <CourseList
            header={'Courses'}
            courses={courses}
          />}
        {courses.actions && <CustomForm action={courses.actions[0]} />}
      </Segment>
    )
  }
}

CoursesPage.propTypes = {
  api: PropTypes.object.isRequired,
  session: PropTypes.object.isRequired
}

export default CoursesPage

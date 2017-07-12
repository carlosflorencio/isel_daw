import React, { Component } from "react";
import PropTypes from "prop-types";
import axios from "../../data/axiosConfig";

import { Segment } from "semantic-ui-react";

import CustomForm from "../shared/CustomForm";
import CourseList from "./CourseList";

import { CourseList as CourseListRequest } from "../../data/ApiContracts";

class CoursesPage extends Component {
  constructor(props) {
    super(props);
    let params = new URLSearchParams(props.location.search);
    this.state = {
      isLoading: true,
      courses: {},
      params: {
        page: params.page || 1,
        limit: params.limit || 5
      }
    };

    this.getData = this.getData.bind(this);
  }

  getData(link) {
    let uri = link || this.props.api.requests[CourseListRequest];
    axios(uri, { params: this.state.params })
      .then(resp => resp.data)
      .then(courses => {
        console.log(courses);
        this.setState({ courses, isLoading: false });
      });
  }

  componentDidUpdate(prevProps) {
    if (
      this.props.session.isAuthenticated ^ prevProps.session.isAuthenticated
    ) {
      this.getData();
    }
  }

  componentDidMount() {
    this.getData();
  }

  render() {
    const { courses, isLoading } = this.state;
    return (
      <Segment basic className="padding-left-right" loading={isLoading}>
        {courses.entities &&
          <CourseList
            header={"Courses"}
            courses={courses}
            getMoreCourses={this.getData}
          />}
        {courses.actions && <CustomForm action={courses.actions[0]} />}
      </Segment>
    );
  }
}

CoursesPage.propTypes = {
  api: PropTypes.object.isRequired,
  session: PropTypes.object.isRequired
};

export default CoursesPage;

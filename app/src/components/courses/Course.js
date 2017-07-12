import React, { Component } from "react";
import PropTypes from "prop-types";
import axios from "../../data/axiosConfig";

import { Segment, Button, Header } from "semantic-ui-react";
import { NavLink } from "react-router-dom";

import SirenHelpers from "../../helpers/SirenHelpers";
import ClassList from "../classes/ClassList";
import CustomForm from "../shared/CustomForm";

import {
  CourseEntry,
  REL_COURSE_CLASSES,
  ADD_CLASS_COURSE
} from "../../data/ApiContracts";

class Course extends Component {
  constructor(props) {
    super(props);
    let params = new URLSearchParams(props.location.search);
    this.state = {
      isLoading: true,
      course: {},
      params: {
        page: params.page || 1,
        limit: params.limit || 5
      }
    };
  }

  getData() {
    let uri = this.props.api.requests[CourseEntry].replace(
      "{id}",
      this.props.match.params.id
    );

    axios(uri)
      .then(resp => resp.data)
      .then(course => {
        console.log(course);
        const teacher = SirenHelpers.getSubEntity(course, "coordinator");
        this.setState({ course, teacher, isLoading: false });
        return SirenHelpers.getLink(course, REL_COURSE_CLASSES);
      })
      .then(href => axios(href))
      .then(resp => resp.data)
      .then(classes => {
        console.log(classes);
        this.setState({ classes });
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
    const { course, teacher, classes, isLoading } = this.state;
    return (
      <Segment basic loading={isLoading}>
        {course.properties &&
          <Header className="padding-left-right" as="h1">
            {course.properties["name"]}
          </Header>}
        {classes &&
          <ClassList
            header={course.properties["acr"] + " Classes"}
            classes={classes}
          />}
        <Segment basic textAlign="center">
          {course.properties &&
            <div>
              <h2>
                Coordinator: {teacher.properties["name"]}
              </h2>
              <h2>
                E-mail: {teacher.properties["email"]}
              </h2>
              <Button
                as={NavLink}
                to={"/teachers/" + teacher.properties["number"]}
              >
                Coordinator Details
              </Button>
            </div>}
          {course.actions &&
            <CustomForm
              action={SirenHelpers.getAction(course, ADD_CLASS_COURSE)}
            />}
        </Segment>
      </Segment>
    );
  }
}

Course.propTypes = {
  session: PropTypes.object.isRequired
};

export default Course;

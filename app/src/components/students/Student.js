import React, { Component } from "react";
import PropTypes from "prop-types";
import axios from "../../data/axiosConfig";

import { Segment, Card, Image } from "semantic-ui-react";

import ClassList from "../classes/ClassList";

import { StudentEntry, REL_STUDENT_CLASSES } from "../../data/ApiContracts";

import SirenHelpers from "../../helpers/SirenHelpers";

class Student extends Component {
  constructor(props) {
    super(props);
    this.state = {
      student: {},
      classes: {}
    };
  }

  componentDidMount() {
    const uri = this.props.api.requests[StudentEntry].replace(
      "{number}",
      this.props.match.params.id
    );

    axios(uri)
      .then(resp => resp.data)
      .then(student => {
        console.log(student);
        this.setState({ student });
        return SirenHelpers.getLink(student, REL_STUDENT_CLASSES);
      })
      .then(href => axios(href))
      .then(resp => resp.data)
      .then(classes => {
        console.log(classes);
        this.setState({ classes });
      });
  }

  render() {
    const { student, classes } = this.state;
    return (
      <Segment basic textAlign="center">
        <Segment className="padding-left-right" color="teal">
          <h1>Student</h1>
          {student.properties &&
            <Card centered>
              <Image src="http://via.placeholder.com/300x150" />
              <Card.Content>
                <Card.Header>
                  {student.properties["name"]}
                </Card.Header>
                <Card.Description>
                  Email: {student.properties["email"]}
                </Card.Description>
              </Card.Content>
            </Card>}
          {classes.entities &&
            <ClassList header={"Classes you attend"} classes={classes} />}
        </Segment>
      </Segment>
    );
  }
}

Student.propTypes = {
  api: PropTypes.object.isRequired,
  session: PropTypes.object.isRequired
};

export default Student;

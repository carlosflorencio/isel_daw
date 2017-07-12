import React, { Component } from "react";
import PropTypes from "prop-types";
import axios from "../../data/axiosConfig";

import SirenHelpers from "../../helpers/SirenHelpers";

import { Segment } from "semantic-ui-react";

import StudentsList from "./StudentsList";
import CustomForm from "../shared/CustomForm";

import {
  ClassStudentsList,
  REMOVE_CLASS_STUDENT,
  ADD_CLASS_STUDENT
} from "../../data/ApiContracts";

class ClassStudents extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoading: true,
      students: {}
    };
  }

  componentDidMount() {
    let uri = this.props.api.requests[ClassStudentsList].replace(
      "{id}",
      this.props.match.params.id
    );

    axios(uri).then(resp => resp.data).then(students => {
      console.log(students);
      this.setState({ students, isLoading: false });
    });
  }

  render() {
    const { students, isLoading } = this.state;
    return (
      <Segment basic loading={isLoading}>
        <StudentsList students={students} actionRel={REMOVE_CLASS_STUDENT} />
        {students.actions &&
          <CustomForm
            action={SirenHelpers.getAction(students, ADD_CLASS_STUDENT)}
          />}
      </Segment>
    );
  }
}

ClassStudents.propTypes = {
  api: PropTypes.object.isRequired
};

export default ClassStudents;

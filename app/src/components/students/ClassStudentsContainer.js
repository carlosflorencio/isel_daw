import { connect } from "react-redux";

import ClassStudents from "./ClassStudents";

const mapStateToProps = (state, ownProps) => {
  return {
    api: state.api
  };
};

export default connect(mapStateToProps)(ClassStudents);

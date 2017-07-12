import { connect } from "react-redux";

import Course from "./Course";

const mapStateToProps = (state, ownProps) => {
  return {
    api: state.api,
    session: state.session
  };
};

export default connect(mapStateToProps)(Course);

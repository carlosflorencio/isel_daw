import { connect } from "react-redux";

import Home from "./Home";

const mapStateToProps = (state, ownProps) => {
  return {
    session: state.session
  };
};

// function mapDispatchToProps (dispatch) {
//   return {

//   }
// }

export default connect(mapStateToProps)(Home);

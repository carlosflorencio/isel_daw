import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { requestLogin, requestLogout } from '../auth/AuthReducer'

import Navbar from './Navbar'

const mapStateToProps = (state, ownProps) => {
  return {
    session: state.session
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators({ requestLogin, requestLogout }, dispatch)
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Navbar)

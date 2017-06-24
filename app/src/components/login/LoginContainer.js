import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import Login from './Login'

import { login } from './AuthReducer'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

function mapDispatchToProps (dispatch) {
  return {
      actions: bindActionCreators({ login }, dispatch)
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Login)
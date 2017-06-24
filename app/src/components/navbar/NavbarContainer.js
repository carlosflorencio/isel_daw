import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { logout } from '../login/AuthReducer'

import Navbar from './Navbar'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

function mapDispatchToProps (dispatch) {
  return {
      actions: bindActionCreators({ logout }, dispatch)
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Navbar)
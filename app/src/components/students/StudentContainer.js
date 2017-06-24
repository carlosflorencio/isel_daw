import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import Student from './Student'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session,
    }
}

export default connect(mapStateToProps)(Student)

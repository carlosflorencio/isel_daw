import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import Teacher from './Teacher'

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api,
        session: state.session
    }
}

export default connect(mapStateToProps)(Teacher)

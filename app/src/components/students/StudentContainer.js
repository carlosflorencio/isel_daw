import { connect } from 'react-redux'

import Student from './Student'

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api,
        session: state.session,
    }
}

export default connect(mapStateToProps)(Student)

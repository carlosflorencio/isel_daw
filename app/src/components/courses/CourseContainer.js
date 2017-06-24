import { connect } from 'react-redux'

import Course from './Course'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

export default connect(mapStateToProps)(Course)
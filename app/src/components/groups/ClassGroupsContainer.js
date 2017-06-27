import {connect} from 'react-redux'

import ClassGroups from './ClassGroups'

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api
    }
}

export default connect(mapStateToProps)(ClassGroups)
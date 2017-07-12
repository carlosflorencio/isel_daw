import {connect} from 'react-redux'

import Group from './Group'

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api
    }
}

export default connect(mapStateToProps)(Group)
import {connect} from 'react-redux'

import ClassGroups from './ClassGroups'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

// function mapDispatchToProps (dispatch) {
//   return {
    
//   }
// }

export default connect(mapStateToProps)(ClassGroups)
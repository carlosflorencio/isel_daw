import {connect} from 'react-redux'

import Class from './Class'

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api,
        session: state.session
    }
}

// function mapDispatchToProps (dispatch) {
//   return {
    
//   }
// }

export default connect(mapStateToProps)(Class)
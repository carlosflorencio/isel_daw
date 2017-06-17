import {connect} from 'react-redux'

import Navbar from './Navbar'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

// function mapDispatchToProps (dispatch) {
//   return {
    
//   }
// }

export default connect(mapStateToProps)(Navbar)
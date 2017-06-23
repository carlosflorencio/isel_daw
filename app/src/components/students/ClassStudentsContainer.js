import {connect} from 'react-redux'

import ClassStudents from './ClassStudents'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

// function mapDispatchToProps (dispatch) {
//   return {
//   }
// }

export default connect(mapStateToProps)(ClassStudents)

import {connect} from 'react-redux'

import CourseList from './CourseList'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}

// function mapDispatchToProps (dispatch) {
//   return {
    
//   }
// }

export default connect(mapStateToProps)(CourseList)
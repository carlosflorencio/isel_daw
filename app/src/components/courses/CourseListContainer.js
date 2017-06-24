import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import CourseList from './CourseList'
import {getCourses} from './CoursesReducer'

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session,
        courses: state.courses
    }
}

function mapDispatchToProps (dispatch) {
  return {
      actions: bindActionCreators({ getCourses }, dispatch)
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(CourseList)
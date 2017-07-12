import { connect } from 'react-redux'

import CoursesPage from './CoursesPage'

const mapStateToProps = (state, ownProps) => {
  return {
    api: state.api,
    session: state.session
  }
}

export default connect(mapStateToProps)(CoursesPage)

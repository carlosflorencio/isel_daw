import { connect } from 'react-redux'

import ClassTeachers from './ClassTeachers'

const mapStateToProps = (state, ownProps) => {
  return {
    api: state.api
  }
}

export default connect(mapStateToProps)(ClassTeachers)

import React from 'react'
import PropTypes from 'prop-types'

class OIDCCallback extends React.Component {
  componentDidMount() {
    this.props.callback() // invoke the callback
  }

  render() {
    return <h5>Wait for redirection.</h5>
  }
}

OIDCCallback.propTypes = {
  callback: PropTypes.func.isRequired
}

export default OIDCCallback

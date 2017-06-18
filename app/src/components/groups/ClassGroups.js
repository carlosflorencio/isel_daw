import React, { Component } from 'react'
import PropTypes from 'prop-types'

class ClassGroups extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const {id} = this.props.match.params
        return (
            <div>
                <h1>Groups of Class {id}</h1>
            </div>
        )
    }
}

export default ClassGroups

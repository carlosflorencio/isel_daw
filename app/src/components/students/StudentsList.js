import React, { Component } from 'react'
import PropTypes from 'prop-types'
import axios from '../../data/axiosConfig'

import { Table } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

import SirenHelpers from '../../helpers/SirenHelpers'

import EntityActionCell from '../shared/EntityActionCell'

class StudentsList extends Component {
  constructor(props) {
    super(props)
    this.state = {}
  }

  render() {
    const { students, actionRel } = this.props
    return (
      <Table celled striped selectable color="teal">
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell colSpan="4" textAlign="center">
              Students
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {students.entities &&
            students.entities.map(student => {
              return (
                <Table.Row key={student.properties['number']}>
                  <Table.Cell collapsing>
                    {student.properties['name']}
                  </Table.Cell>
                  <Table.Cell collapsing>
                    {student.properties['email']}
                  </Table.Cell>
                  <Table.Cell collapsing>
                    <NavLink to={'/students/' + student.properties['number']}>
                      Details
                    </NavLink>
                  </Table.Cell>
                  <EntityActionCell
                    onClick={() => {
                      let conf = SirenHelpers.createAxiosConfig(
                        student,
                        actionRel
                      )
                      axios(conf.url, { method: conf.method })
                    }}
                    action={SirenHelpers.getAction(student, actionRel)}
                  />
                </Table.Row>
              )
            })}
        </Table.Body>
      </Table>
    )
  }
}

StudentsList.propTypes = {
  students: PropTypes.object.isRequired
}

export default StudentsList

import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Table, Menu } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'
import TablePagingFooter from '../shared/TablePagingFooter'

import SirenHelpers from '../../helpers/SirenHelpers'

class CourseList extends Component {
  constructor(props) {
    super(props)
    this.state = {
      prevLink: SirenHelpers.getLink(props.courses, 'prev'),
      nextLink: SirenHelpers.getLink(props.courses, 'next')
    }
  }

  componentWillUpdate(nextProps) {
    if (nextProps.courses !== this.props.courses) {
      this.setState({
        prevLink: SirenHelpers.getLink(nextProps.courses, 'prev'),
        nextLink: SirenHelpers.getLink(nextProps.courses, 'next')
      })
    }
  }

  getLink(sirenLink){
    if(!sirenLink){
      return
    }

    let params = new URLSearchParams(sirenLink.split("?")[1])
    let page = params.get('page')
    let limit = params.get('limit')

    return '/courses?page='+page+'&limit='+limit
  }

  render() {
    const { courses } = this.props
    return (
      <Table celled striped selectable color="teal">
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Acronym</Table.HeaderCell>
            <Table.HeaderCell>Name</Table.HeaderCell>
            <Table.HeaderCell>Coordinator</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {courses.entities &&
            courses.entities.map(course => {
              return (
                <Table.Row key={course.properties['id']}>
                  <Table.Cell collapsing>
                    {course.properties['acr']}
                  </Table.Cell>
                  <Table.Cell collapsing>
                    <NavLink to={'/courses/' + course.properties['id']}>
                      {course.properties['name']}
                    </NavLink>
                  </Table.Cell>
                  <Table.Cell collapsing>
                    {course.entities[0].properties.name}
                  </Table.Cell>
                </Table.Row>
              )
            })}
        </Table.Body>
        {
          (this.state.prevLink || this.state.nextLink) && 
          <TablePagingFooter
            prevLink={this.getLink(this.state.prevLink)}
            nextLink={this.getLink(this.state.nextLink)}
          />
        }
      </Table>
    )
  }
}

CourseList.propTypes = {
  header: PropTypes.string.isRequired,
  courses: PropTypes.object.isRequired
}

export default CourseList

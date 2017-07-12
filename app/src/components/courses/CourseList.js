import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Table } from 'semantic-ui-react'
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
          this.props.getMoreCourses && 
          <TablePagingFooter
            getMoreData={this.props.getMoreCourses}
            prevLink={SirenHelpers.getLink(courses, 'prev')}
            nextLink={SirenHelpers.getLink(courses, 'next')}
          />
        }
      </Table>
    )
  }
}

CourseList.propTypes = {
  header: PropTypes.string.isRequired,
  courses: PropTypes.object.isRequired,
  getMoreCourses: PropTypes.func
}

export default CourseList

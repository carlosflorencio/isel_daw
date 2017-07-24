import React from 'react'
import { Menu, Table } from 'semantic-ui-react'
import { NavLink } from 'react-router-dom'

export default ({ prevLink, nextLink }) => {
  return (
    <Table.Footer fullWidth>
      <Table.Row>
        <Table.HeaderCell colSpan="3">
          <Menu borderless>
            <PrevLinkMenu prevLink={prevLink} />
            <NextLinkMenu nextLink={nextLink} />
          </Menu>
        </Table.HeaderCell>
      </Table.Row>
    </Table.Footer>
  )
}

const NextLinkMenu = ({ nextLink }) => {
  if (!nextLink) {
    return (
      <Menu.Item
        name="Next"
        disabled={true}
        position="right"
      />
    )
  }
  return (
    <Menu.Item
      as={NavLink}
      to={nextLink}
      position="right"
      disabled={!nextLink}
      name="Next"
    />
  )
}

const PrevLinkMenu = ({ prevLink }) => {
  if (!prevLink) {
    return (
      <Menu.Item
        name="Previous"
        disabled={true}
      />
    )
  }
  return (
    <Menu.Item
      as={NavLink}
      to={prevLink}
      name="Previous"
      disabled={!prevLink}
    />
  )
}

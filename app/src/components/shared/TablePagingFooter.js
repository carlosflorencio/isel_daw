import React from "react";
import { Menu, Table } from "semantic-ui-react";

export default ({ getMoreData, prevLink, nextLink }) => {
  return (
    <Table.Footer fullWidth>
      <Table.Row>
        <Table.HeaderCell colSpan="3">
          <Menu borderless>
            <Menu.Item
              name="Previous"
              disabled={!prevLink}
              onClick={() => getMoreData(prevLink)}
            />
            <Menu.Item
              position="right"
              disabled={!nextLink}
              name="Next"
              onClick={() => getMoreData(nextLink)}
            />
          </Menu>
        </Table.HeaderCell>
      </Table.Row>
    </Table.Footer>
  );
};

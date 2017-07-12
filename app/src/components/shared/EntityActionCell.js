import React from "react";

import { Table, Button } from "semantic-ui-react";

const EntityActionCell = ({ action, onClick }) => {
  return (
    action &&
    onClick &&
    <Table.Cell collapsing>
      <Button onClick={onClick}>
        {action.title}
      </Button>
    </Table.Cell>
  );
};

export default EntityActionCell;

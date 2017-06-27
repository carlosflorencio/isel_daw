import React, { Component } from 'react'

import { Route, BrowserRouter as Router, Switch, NavLink } from 'react-router-dom'
import { Segment, Menu, Grid } from 'semantic-ui-react'
import classRoutes from './routes'

import ClassContainer from './ClassContainer'

class ClassPage extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        const { id } = this.props.match.params
        return (
            <Segment basic textAlign='center'>
                <Grid stackable textAlign='center'>
                    <Grid.Row>
                        <Grid.Column width={3}>
                            <ClassMenu id={id} />
                        </Grid.Column>
                        <Grid.Column width={13}>
                            <Router>
                                <Switch>
                                    <Route exact path='/classes/:id' component={ClassContainer} />
                                    {classRoutes.map((route, i) => 
                                        <Route
                                            key={i}
                                            path={route.path}
                                            exact={route.exact}
                                            component={route.component}
                                        />
                                    )}
                                </Switch>
                            </Router>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
            </Segment>
        )
    }
}

const ClassMenu = ({ id }) =>
    <Menu vertical>
        <Menu.Item
            as={NavLink}
            to={'/classes/' + id + '/teachers'}
            name='teachers'>
            Teachers
        </Menu.Item>
        <Menu.Item
            as={NavLink}
            to={'/classes/' + id + '/students'}
            name='students' >
            Students
        </Menu.Item>
        <Menu.Item
            as={NavLink}
            to={'/classes/' + id + '/groups'}
            name='groups'>
            Groups
        </Menu.Item>
    </Menu>


export default ClassPage

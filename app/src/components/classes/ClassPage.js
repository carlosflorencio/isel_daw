import React from 'react'

import { Route, Switch, NavLink } from 'react-router-dom'
import { Segment, Menu, Grid } from 'semantic-ui-react'
import classRoutes from './routes'

import PageNotFound from '../shared/PageNotFound'
import ClassContainer from './ClassContainer'

const ClassPage = (props) => {
    const { id } = props.match.params
    return (
        <Segment basic>
            <Grid stackable textAlign='center'>
                <Grid.Row>
                    <Grid.Column width={3}>
                        <ClassMenu id={id} />
                    </Grid.Column>
                    <Grid.Column width={13}>
                        <Switch>
                            <Route exact path='/classes/:id/info' component={ClassContainer} />
                            {classRoutes.map((route, i) =>
                                <Route
                                    key={i}
                                    path={route.path}
                                    exact={route.exact}
                                    component={route.component}
                                />
                            )}
                            <Route component={PageNotFound} />
                        </Switch>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    )
}

const ClassMenu = ({ id }) =>
    <Menu vertical color='teal'>
        <Menu.Item
            as={NavLink}
            to={'/classes/' + id + '/info'}
            name='info'>
            Class Details
        </Menu.Item>
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

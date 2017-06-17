import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import { Container } from 'semantic-ui-react'

import PrivateRoute from './shared/PrivateRoute'

import Navbar from './navbar/NavbarContainer'
import Home from './home/HomeContainer'
import Course from './courses/Course'
import CourseList from './courses/CourseList'
import Login from './login/Login'
import PageNotFound from './shared/PageNotFound'
import Unauthorized from './shared/Unauthorized'
import privateRoutes from '../routes'

class MainContent extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        return (
            <Container fluid>
                <Navbar />
                <Router>
                    <Switch>
                        <Route exact path='/' component={Home} />
                        <Route exact path='/courses' component={CourseList} />
                        <Route exact path='/courses/:id' component={Course} />
                        <Route exact path='/login' component={Login} />
                        {privateRoutes.map((route, i) =>
                            (<PrivateRoute
                                key={i}
                                path={route.path}
                                exact={route.exact}
                                component={route.component}
                                session={this.props.session}
                                minRole={route.minRole}
                                routes={route.routes}
                            />)
                        )}
                        <Route path="/unauthorized" component={Unauthorized} />
                        <Route component={PageNotFound} />
                    </Switch>
                </Router>
            </Container>
        )
    }
}

const mapStateToProps = (state, ownProps) => {
    return {
        session: state.session
    }
}


export default connect(mapStateToProps)(MainContent)

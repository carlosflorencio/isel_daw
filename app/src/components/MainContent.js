import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import { Container } from 'semantic-ui-react'

import PrivateRoute from './shared/PrivateRoute'

import Auth from './auth/Auth'
import Navbar from './navbar/NavbarContainer'
import NoServerResponse from './shared/NoServerResponse'
import Home from './home/HomeContainer'
import Course from './courses/CourseContainer'
import CourseList from './courses/CourseListContainer'
import Login from './login/LoginContainer'
import PageNotFound from './shared/PageNotFound'
import Unauthorized from './shared/Unauthorized'
import privateRoutes from '../routes'

import { getHomepage } from './ApiReducer'
import { updateLoginStatus } from './auth/AuthReducer'

class MainContent extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        this.props.actions.getHomepage()
        this.props.actions.updateLoginStatus()
    }

    render() {
        return (
            <Container className='menu-margin' fluid>
                {
                    this.props.api.hasData &&
                    <Router>
                        <div>
                            <Navbar />
                            <Switch>
                                <Route exact path='/' component={Home} />
                                <Route path='/auth' component={Auth} />
                                <Route exact path='/courses' component={CourseList} />
                                <Route exact path='/courses/:id' component={Course} />
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
                        </div>
                    </Router>
                }
                {
                    !this.props.api.hasData && this.props.api.failedLoadingData &&
                    <NoServerResponse />
                }
            </Container>
        )
    }
}

const mapStateToProps = (state, ownProps) => {
    return {
        api: state.api,
        session: state.session
    }
}


const mapDispatchToProps = dispatch => {
    return {
        actions: bindActionCreators({ getHomepage, updateLoginStatus }, dispatch)
    }
}



export default connect(mapStateToProps, mapDispatchToProps)(MainContent)


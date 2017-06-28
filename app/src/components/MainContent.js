import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import { Container } from 'semantic-ui-react'

import PrivateRoute from './shared/PrivateRoute'

import Navbar from './navbar/NavbarContainer'
import Home from './home/HomeContainer'
import Course from './courses/CourseContainer'
import CourseList from './courses/CourseListContainer'
import Login from './login/LoginContainer'
import PageNotFound from './shared/PageNotFound'
import Unauthorized from './shared/Unauthorized'
import privateRoutes from '../routes'

import { getHomepage } from './ApiReducer'

class MainContent extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    componentDidMount() {
        this.props.actions.getHomepage()
    }

    render() {
        return (
            <Container className='menu-margin' fluid>
                <Router>
                    <div>
                        <Navbar />
                        {
                            this.props.api.hasData &&
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
                        }
                    </div>
                </Router>
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
        actions: bindActionCreators({ getHomepage }, dispatch)
    }
}



export default connect(mapStateToProps, mapDispatchToProps)(MainContent)


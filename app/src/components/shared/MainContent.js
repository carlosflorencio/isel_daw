import React, { Component } from 'react'

import { connect } from 'react-redux'

import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import PrivateRoute from './PrivateRoute'

import Navbar from './Navbar'
import Home from '../home/Home'
import Login from '../login/Login'
import PageNotFound from './PageNotFound'
import Unauthorized from './Unauthorized'
import privateRoutes from '../../routes'

class MainContent extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }
    }

    render() {
        return (
            <div>
                <Navbar />
                <Router>
                    <Switch>
                        <Route exact path='/' component={Home} />
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
            </div>
        )
    }
}

const mapStateToProps = (state, ownProps) => {
    console.log(state)
    return {
        session: state.session
    }
}


export default connect(mapStateToProps)(MainContent)


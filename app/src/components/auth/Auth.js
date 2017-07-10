import React from 'react'
import { Route, Switch } from 'react-router-dom'
import routes from './routes'
import PageNotFound from '../shared/PageNotFound'

const Auth = () => {
  return (
    <div>
      <Switch>
        {routes.map((route, i) => (
          <Route
            key={i}
            path={route.path}
            exact={route.exact}
            render={route.render}
          />
        ))}
        <Route component={PageNotFound} />}
      </Switch>
    </div>
  )
}
export default Auth

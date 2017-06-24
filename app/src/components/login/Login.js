import React, { Component } from 'react'
import PropTypes from 'prop-types'

import { Container, Segment, Form, Button, Header } from 'semantic-ui-react'
import { Redirect } from 'react-router-dom'

class Login extends Component {
    constructor(props) {
        super(props)
        this.state = {
            user:{
                email: '',
                password: ''
            }
        }

        this.submitLogin = this.submitLogin.bind(this)
        this.onStateChange = this.onStateChange.bind(this)
    }

    onStateChange(event, {name, value}){
        let user = this.state.user
        user[name] = value
        this.setState({user})
    }

    submitLogin() {
        this.props.actions.login(this.state.user)
    }

    render() {
        const { session } = this.props
        const { user } = this.state
        if (session.isAuthenticated) {
            return (
                <Redirect to='/' />
            )
        }
        return (
            <Container className='padding-left-right' fluid>
                <Segment color='teal' secondary>
                    <Segment basic compact textAlign='right'>
                        <Header as='h1' textAlign='left'>Welcome Back</Header>
                        <Form widths='equal'>
                            <Form.Input
                                label='Email:'
                                placeholder='Email'
                                name='email'
                                value={user.email}
                                onChange={this.onStateChange}
                                inline
                            />
                            <Form.Input
                                label='Password:'
                                placeholder='Password'
                                type='password'
                                name='password'
                                value={user.password}
                                onChange={this.onStateChange}
                                inline
                            />
                        </Form> 
                        <Button className='margin-top' onClick={this.submitLogin}>Login</Button>
                    </Segment>
                </Segment>
            </Container>
        )
    }
}

Login.propTypes = {
    session: PropTypes.object.isRequired
}

export default Login

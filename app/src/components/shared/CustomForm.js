import React, { Component } from 'react'
import axios from 'axios'

import {Form, Button} from 'semantic-ui-react'

class CustomForm extends Component {
    constructor(props) {
        super(props)
        this.state = {
        }

        this.onChange = this.onChange.bind(this)
        this.post = this.post.bind(this)
    }

    onChange(event, {name, value}) {
        var field = this.props.action.fields.find(field => field.name === name)
        field.value = formHelper(field.type, value)
        this.setState(this.state)
    }

    post() {
        const { action } = this.props
        var data = {}
        if(action.fields)
            action.fields.forEach(field => {
                data[field.name] = field.value
            })
        axios.post(action.href, data)
            .then(resp => console.log(resp.data))
    }

    render() {
        const {action} = this.props
        return (
            <div>
                <h1>{action.title}</h1>
                <Form>
                    {
                        action.fields &&
                        action.fields.map((field, i) => {
                            return <Form.Input
                                key={i}
                                label={field.title}
                                name={field.name}
                                type={field.type}
                                value={field.value}
                                onChange={this.onChange}
                                inline
                            />
                        })
                    }
                    <Button type='submit' onClick={this.post}>Submit</Button>
                </Form>
            </div>
        )
    }
}

function formHelper(type, value){
    switch(type){
        // case 'number':
        //     return Number(value)
        case 'checkbox':
            return value === 'on'
        default:
            return value
    }
}

export default CustomForm

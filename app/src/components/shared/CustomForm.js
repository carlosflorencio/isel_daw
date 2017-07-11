import React, { Component } from 'react'
import axios from '../../data/axiosConfig'

import {Form, Button} from 'semantic-ui-react'

class CustomForm extends Component {
    constructor(props) {
        super(props)
        this.state = {
            fields: props.action.fields
        }

        this.onChange = this.onChange.bind(this)
        this.post = this.post.bind(this)
    }

    onChange(event, {name, value}) {
        let field = this.state.fields.find(field => field.name === name)
        field.value = formHelper(field.type, value)
        this.setState({fields: this.state.fields})
    }

    post() {
        const { fields } = this.state
        var data = {}
        if(fields)
            fields.forEach(field => {
                data[field.name] = field.value
            })
        axios(this.props.action.href, { method: this.props.action.method, data: data })
            .then(resp => console.log(resp.data))
    }

    render() {
        const {action} = this.props
        const {fields} = this.state
        return (
            <div>
                <h1>{action.title}</h1>
                <Form>
                    {
                        fields &&
                        fields.map((field, i) => {
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

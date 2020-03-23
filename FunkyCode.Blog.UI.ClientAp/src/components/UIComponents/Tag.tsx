import React, { Component } from 'react';
import { CSSProperties } from 'react'

interface Props {
    name: string,
    customStyle: CSSProperties,
    tagSelected(tag: string): void;  
}

interface State  {
    
}

export class Tag extends Component<Props, State>  {

    render() {
        const {name, customStyle} = this.props;
        return <div style={customStyle} onClick={this.handleClick.bind(this)} > {name}</div>
    }

    handleClick() {
        this.props.tagSelected(this.props.name);
    }
}






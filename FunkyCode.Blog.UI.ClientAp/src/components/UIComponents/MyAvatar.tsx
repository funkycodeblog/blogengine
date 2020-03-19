import React, { Component } from 'react';

interface Props {
    size: number  
}

interface State  {
    
}

export class MyAvatar extends Component<Props, State>  {
    

    render() {

        const radius = this.props.size / 2.0;
        
        const radiusInPx = `${radius}px`;
        const sizeInPx = `${this.props.size}px`;

        const url = require('../../assets/images/maciej-avatar-300.jpg');
        
        return <img alt="?" src={url} style={{borderRadius: radiusInPx, width: sizeInPx, height: sizeInPx}} ></img>
    }
}


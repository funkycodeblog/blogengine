import React, { Component } from 'react';



interface Props {
    
    height: number

}

export class Spacer extends Component<Props, {}> {

    render() {
       
        const heightStr = `${this.props.height}px`;
        return <div style={{height: heightStr }} />
    }

   
    
}


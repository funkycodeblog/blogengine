import React, { Component } from 'react';
import { Button } from '@material-ui/core';

interface Props {
    title: string,
    onClickEvent: () => void
    buttonType : "primary" | "secondary" | "blank" | "border"
    customStyle? : object
}

export class FunkyButton extends Component<Props, {}> {

    render() {
        const buttonStyle = this.getButtonStyle(this.props.buttonType)
        const combined = { ...baseButtonStyle, ...buttonStyle, ...this.props.customStyle}

        return <Button onClick={this.props.onClickEvent} style={combined}> {this.props.title} </Button>
    }

    getButtonStyle(buttonType: string)
    {
        if (buttonType === 'primary') return primaryButtonStyle;
        if (buttonType === 'secondary') return secondaryButtonStyle;
        if (buttonType === 'blank') return blankButtonStyle;
        if (buttonType === 'border') return noFillButtonStyle;
        return null;
    }

}


const baseButtonStyle = {
    minWidth: '120px',
    height: '40px',
    letterSpacing: '0.1em',
    fontSize: '14px',
    borderRadius: '100px',
    fontWeight: 'bold' as 'bold',
    fontStyle: 'normal',
}

const primaryButtonStyle = {
    
    border: 'none',
    color: '#FFFFFF',
    background: '#FF000F',

}

const secondaryButtonStyle =
{
    border: '0',
    background: '#D2D2D2',
    color: '#FFFFFF',
};

const blankButtonStyle =
{
  
    color: '#000000',
    
};

const noFillButtonStyle =
{
    border: '2px solid',
    borderColor: '#000000',
    color: '#000000',
   
};
import React, { Component } from 'react';
import { CSSProperties } from 'react'
import { BlogEnginePaths } from '../../config/BlogEngineSettings';
import { Link } from 'react-router-dom';

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
        const path = BlogEnginePaths.ResolveTagPath(name);
        return <Link style={customStyle} to={path}>{name}</Link>        
        
    }
  
}






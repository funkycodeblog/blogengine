import React, { Component, CSSProperties } from 'react';
import { isNullOrUndefined } from 'util';
import { Tag } from './Tag';

interface Props  {
  tags?: string[],
  tagSelected(tag: string): void;  
  type: 'navi' | 'article'
}

interface State {
 
}

export class TagBox extends Component<Props, State>  {

  render() {

    const { tags, type } = this.props;

    if ( isNullOrUndefined(tags) ) return null;

    const tagStyle : CSSProperties = type === 'navi' ? tagNaviStyle : tagArticleStyle;

    return <div style={ boxStyle }>

      { tags.map( tag => <Tag key={tag} name={tag} customStyle={tagStyle} tagSelected={this.tagSelected.bind(this)} /> )}

      </div>
   
  }

    tagSelected (tagName: string) {
        this.props.tagSelected(tagName);
    }

}

const tagNaviStyle : CSSProperties = { 
    
    display: 'inline-block',
    
    borderWidth: 1, 
    borderStyle: 'solid', 
    borderColor: 'white',
    margin: '2px',
    
    color: 'white',
    marginLeft: '1px', 
    marginRight: '1px', 
    paddingLeft: '1px', 
    paddingRight: '1px',
    
}

const tagArticleStyle : CSSProperties = { 
    
    display: 'inline-block',
    
    borderWidth: 1, 
    borderStyle: 'solid', 
    borderColor: 'black',
    margin: '2px',
    
    color: 'black',
    marginLeft: '1px', 
    marginRight: '1px', 
    paddingLeft: '1px', 
    paddingRight: '1px',
    
}

const boxStyle : CSSProperties = {
    
    display: 'block',
    width: '100%',
}
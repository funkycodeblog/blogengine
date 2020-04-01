import React, { Component } from 'react';
import { BlogInfoModel } from '../model/BlogInfoModel';
import { Typography, Button } from '@material-ui/core';
import { resolvePostDate } from '../tools/tools';
import { TagBox } from './UIComponents/TagBox';
import { FunkyStyles } from '../config/FunkyStyles';


interface Props {
    
    blogInfo: BlogInfoModel
    handleBlogInfoSelected: (id: string) => void;
    handleTagSelected: (tag: string) => void;

}

export class BlogInfoComponent extends Component<Props, {}> {

    render() {
    
        const {blogInfo} = this.props;
       
        

        return <div style={{width: '750px'}}>

            < >
            <Typography display='inline' style={{color: FunkyStyles.GrayColor}}>{ resolvePostDate(blogInfo.published)}  </Typography>
            <div style={{display: 'inline-block'}}>
            <TagBox type='article' tags={blogInfo.tags} tagSelected={this.props.handleTagSelected}  />
            </div>
            </ >
        
           <Typography  variant="h4" onClick={this.handleClick.bind(this)} style={{cursor: 'pointer'}} >{blogInfo.title}</Typography>
           < >
           <Typography display='inline'>{blogInfo.text}</Typography>
           <Typography display='inline'>  </Typography>
           <Typography display='inline' onClick={this.handleClick.bind(this)} style={{cursor: 'pointer', fontWeight: 500, color: FunkyStyles.GrayColor }} >Read more...</Typography>
           </ >
        </div>
    }

    handleClick() : void{
        
        this.props.handleBlogInfoSelected(this.props.blogInfo.id);
    }
   
    
}





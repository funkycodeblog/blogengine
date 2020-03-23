import React, { Component } from 'react';
import { BlogInfoModel } from '../model/BlogInfoModel';
import { Typography, Button } from '@material-ui/core';
import { resolvePostDate } from '../tools/tools';
import { TagBox } from './UIComponents/TagBox';



interface Props {
    
    blogInfo: BlogInfoModel
    handleBlogInfoSelected: (id: string) => void;
    handleTagSelected: (tag: string) => void;

}

export class BlogInfoComponent extends Component<Props, {}> {

    render() {
    
        const {blogInfo} = this.props;
       
        

        return <div style={{width: '750px'}}>
        <Typography style = {{color: 'gray'}} >{ resolvePostDate(blogInfo.published)}  </Typography>

        <TagBox type='article' tags={blogInfo.tags} tagSelected={this.props.handleTagSelected}  />
        
           <Typography  variant="h4" >{blogInfo.title}</Typography>
           <Typography>{blogInfo.text}</Typography>
           <Button onClick={this.handleClick.bind(this)} variant="outlined">Read more</Button>
        </div>
    }

    handleClick() : void{
        
        this.props.handleBlogInfoSelected(this.props.blogInfo.id);
    }

    
    
}




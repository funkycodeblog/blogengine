import React, { Component } from 'react';
import { BlogInfoModel } from '../model/BlogInfoModel';
import { Typography, Button } from '@material-ui/core';
import { resolvePostDate } from '../tools/tools';



interface Props {
    
    blogInfo: BlogInfoModel
    handleBlogInfoSelected: (id: string) => void;

}

export class BlogInfoComponent extends Component<Props, {}> {

    render() {
    
        const {blogInfo} = this.props;
       
        return <div style={{width: '750px'}}>
        <Typography style = {{color: 'gray'}} >{ resolvePostDate(blogInfo.published)}  </Typography>
           <Typography  variant="h4" >{blogInfo.title}</Typography>
           <Typography>{blogInfo.text}</Typography>
           <Button onClick={this.handleClick.bind(this)} variant="outlined">Read more</Button>
        </div>
    }

    handleClick() : void{
        
        this.props.handleBlogInfoSelected(this.props.blogInfo.id);
    }
    
}




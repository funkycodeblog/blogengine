import React, { Component } from 'react';
import { ArchiveArticleDto } from '../../model/ArchiveArticleDto';
import { Link } from 'react-router-dom';
import { Typography } from '@material-ui/core';

interface Props {
    article: ArchiveArticleDto;
}

interface State  {
    
}

export class ArchiveTitleComponent extends Component<Props, State>  {

    render() {
        const {article} = this.props;
        return <Link to={`/post/${article.id}`}><Typography>{article.title}</Typography> </Link> 
    }
}
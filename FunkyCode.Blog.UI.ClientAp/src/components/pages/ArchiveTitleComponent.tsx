import React, { Component } from 'react';
import { ArchiveArticleDto } from '../../model/ArchiveArticleDto';

interface Props {
    article: ArchiveArticleDto;
}

interface State  {
    
}

export class ArchiveTitleComponent extends Component<Props, State>  {

    render() {
       
        const {article} = this.props;
        return <div>{article.title}</div>
    }
}
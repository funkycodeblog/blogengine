import React, { Component } from 'react';
import { ArchiveYearDto } from '../../model/ArchiveYearDto';
import { Typography } from '@material-ui/core';

interface Props {
    year: ArchiveYearDto;
}

interface State  {
    
}

export class ArchiveYearComponent extends Component<Props, State>  {

    render() {
        const {year} = this.props;
       
        return <Typography variant = 'h4'>{year.year}</Typography>
    }
}
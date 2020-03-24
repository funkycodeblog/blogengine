import React, { Component } from 'react';
import { ArchiveYearDto } from '../../model/ArchiveYearDto';

interface Props {
    year: ArchiveYearDto;
}

interface State  {
    
}

export class ArchiveYearComponent extends Component<Props, State>  {

    render() {
        const {year} = this.props;
        console.log(year);
        return <div>{year.year}</div>
    }
}
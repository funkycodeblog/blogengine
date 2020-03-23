import React, { Component } from 'react';
import BlogPostPage from './BlockPostPage';
import { IAppState } from '../../redux/Store';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { getBlogPost, getArchives } from '../../redux/Thunks';
import { connect } from 'react-redux';
import { ArchiveYearDto } from '../../model/ArchiveYearDto';
import { IFunkyState } from '../../redux/State';

interface Props {
    archives?: ArchiveYearDto[];
    dispatch: ThunkDispatch<any, any, AnyAction>;
}

interface State  {
    
}

class ArchivesPage extends Component<Props, State>  {

    componentDidMount()
    {
        this.props.dispatch(getArchives());
    }

    render() {
       
        console.log(this.props.archives);
        return <div>:)</div>
    }
}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
   
     };
 };
  
  const mapStateToProps = (store: IAppState) => {
    
    const state : IFunkyState = store.funkyState;
    return {
        archives: state.archives
    };
  
  };

  export default connect(mapStateToProps, mapDispatchToProps)(ArchivesPage);
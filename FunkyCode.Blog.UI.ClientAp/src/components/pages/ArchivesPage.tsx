import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AnyAction } from 'redux';
import { ThunkDispatch } from 'redux-thunk';
import { isNullOrUndefined } from 'util';
import { ArchiveYearDto } from '../../model/ArchiveYearDto';
import { IFunkyState } from '../../redux/State';
import { IAppState } from '../../redux/Store';
import { getArchives } from '../../redux/Thunks';
import { ArchiveYearComponent } from './ArchiveYearComponent';
import { ArchiveTitleComponent } from './ArchiveTitleComponent';
import { Spacer } from '../Spacer';

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
        
        const {archives} = this.props;
        if (isNullOrUndefined(archives)) return null;

        return <div style={{paddingTop: '30px'}}>
            { archives.map(y => {

                return <div key={y.year} > 
                    <ArchiveYearComponent year={y} />
                    {
                        y.articles.map(a => <ArchiveTitleComponent key = {a.id} article = {a} /> )
                    }
                    <Spacer height={20} />

                </div>

            }
                

               

            )}

        </div>
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
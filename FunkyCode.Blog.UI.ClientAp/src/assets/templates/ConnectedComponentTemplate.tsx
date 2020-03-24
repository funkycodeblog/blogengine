import React, { Component } from 'react';
import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
}

interface State  {
    
}

class ConnectedComponentTemplate extends Component<Props, State>  {

    render() {
       
        return <div />
    }
}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
     };
 };
  
const mapStateToProps = (store: IAppState) => {
    
    var state : IFunkyState = store.funkyState;
  
};

export default connect(mapStateToProps, mapDispatchToProps)(ConnectedComponentTemplate);
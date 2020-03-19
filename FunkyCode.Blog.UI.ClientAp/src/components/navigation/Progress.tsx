import { Component } from 'react';
import React from 'react'
import { CircularProgress } from '@material-ui/core';
import { connect } from 'react-redux';
import { IAppState } from '../../redux/Store';

interface Props   {
  isLoading: boolean;
}

type State = {
}

class FunkyProgress extends Component<Props, State>  {

  render() {

    const { isLoading} = this.props;

    return isLoading && <CircularProgress style={{position: 'absolute', left: '50%', top: '50%', zIndex: 1000, color: '#ff000f'}} />  
  }

}

const mapStateToProps = (store: IAppState) => {

  return {
    isLoading: store.funkyState.isLoading
  };
};

export default connect(mapStateToProps)(FunkyProgress);

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AnyAction } from 'redux';
import { ThunkDispatch } from 'redux-thunk';

import { IAppState } from '../../redux/Store';
import { BlogInfoModel } from '../../model/BlogInfoModel';
import { IFunkyState } from '../../redux/State';
import { getBlogInfos } from '../../redux/Thunks';
import BlogInfosContainer from '../pages/BlogInfosContainer';
import { isNullOrUndefined } from 'util';

interface Props {

  blogInfos?: BlogInfoModel[],
  dispatch: ThunkDispatch<any, any, AnyAction>

}

interface State {

}

export class BlogInfosPage extends Component<Props, State>  {

  componentDidMount() {
    this.props.dispatch(getBlogInfos());
    console.log(this.props);
  }

  render() {
    const { blogInfos } = this.props;
    if (isNullOrUndefined(blogInfos)) return null;
    return blogInfos && <BlogInfosContainer blogInfos={blogInfos} />
  }

}

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
  return {
    dispatch
  };
};

const mapStateToProps = (store: IAppState) => {

  const state: IFunkyState = store.funkyState;

  return {
    blogInfos: state.blogInfos
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(BlogInfosPage);





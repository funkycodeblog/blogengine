import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AnyAction } from 'redux';
import { ThunkDispatch } from 'redux-thunk';

import { IAppState } from '../../redux/Store';
import { BlogInfoModel } from '../../model/BlogInfoModel';
import { IFunkyState } from '../../redux/State';
import { getArticlesBySearchAction } from '../../redux/Thunks';
import BlogInfosContainer from './BlogInfosContainer';
import { RouteComponentProps } from 'react-router-dom';
import { isNullOrUndefined } from 'util';

type TParams = {
  search : string;
}

interface Props extends RouteComponentProps<TParams> {
  blogInfos?: BlogInfoModel[],
  dispatch: ThunkDispatch<any, any, AnyAction>
}

interface State {

}

export class BlogInfosBySearchPage extends Component<Props, State>  {

  componentDidMount() {
    const {search} = this.props.match.params;
    this.props.dispatch(getArticlesBySearchAction(search));
  }

  componentWillReceiveProps(newProps : Props ) {

    const oldTag = this.props.match.params.search;
    const {search} = newProps.match.params;

    if (oldTag !== search)
    {
      this.props.dispatch(getArticlesBySearchAction(search));
    }
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

export default connect(mapStateToProps, mapDispatchToProps)(BlogInfosBySearchPage);





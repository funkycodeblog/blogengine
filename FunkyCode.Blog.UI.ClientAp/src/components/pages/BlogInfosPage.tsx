import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AnyAction } from 'redux';
import { ThunkDispatch } from 'redux-thunk';

import { IAppState } from '../../redux/Store';
import { BlogInfoModel } from '../../model/BlogInfoModel';
import { IFunkyState } from '../../redux/State';
import { getBlogInfos, getArticlesBySearchAction, getArticlesByTagAction } from '../../redux/Thunks';
import BlogInfosContainer from '../pages/BlogInfosContainer';
import { isNullOrUndefined } from 'util';
import { RouteComponentProps } from 'react-router-dom';
import * as QueryString from 'query-string';

interface Props extends RouteComponentProps {

  blogInfos?: BlogInfoModel[],
  dispatch: ThunkDispatch<any, any, AnyAction>

}

interface State {
  currentKey?: string;
  currentValue: string | string[] | null | undefined;
}

export class BlogInfosPage extends Component<Props, State>  {

  state = { currentKey: '', currentValue: '' }

  componentDidMount() {

    this.executeDispatch(this.props);

  }

  componentWillReceiveProps(newProps: Props) {

    this.executeDispatch(newProps);

  }

  executeDispatch(props: Props) {
    const params = QueryString.parse(props.location.search);
    console.log('params', params);

    if (params['search'] !== undefined) {
      const search = params['search'];

      if (!(this.state.currentKey === 'search' && this.state.currentValue === search)) {
        this.setState({ currentKey: 'search', currentValue: search });
        props.dispatch(getArticlesBySearchAction(search));
      }

      return;
    }

    if (params['tag'] !== undefined) {
      const tag = params['tag'];

      if (!(this.state.currentKey === 'tag' && this.state.currentValue === tag)) {
        this.setState({ currentKey: 'tag', currentValue: tag });
        props.dispatch(getArticlesByTagAction(tag));
      }
      return;
    }


    if (!(this.state.currentKey === 'all' && this.state.currentValue === 'all')) {
      this.setState({ currentKey: 'all', currentValue: 'all' });
      this.props.dispatch(getBlogInfos());
    }
  }


  render() {
    const { blogInfos } = this.props;
    if (isNullOrUndefined(blogInfos)) return null;
    return <BlogInfosContainer blogInfos={blogInfos} />
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





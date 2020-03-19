import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { AnyAction } from 'redux';
import { ThunkDispatch } from 'redux-thunk';

import { BlogEngineSettings } from '../../config/BlogEngineSettings';
import { IAppState } from '../../redux/Store';
import { BlogInfoModel } from '../../model/BlogInfoModel';
import { BlogInfoComponent } from '../BlogInfoComponent';
import { IFunkyState } from '../../redux/State';
import { isNullOrUndefined } from 'util';
import { Spacer } from '../Spacer';
import { getBlogPost } from '../../redux/Thunks';

interface Props  {

  blogInfos?: BlogInfoModel[],
  dispatch: ThunkDispatch<any, any, AnyAction>
 
}



interface State {

  isRedirectToBlog: boolean
}

class BlogInfosPage extends Component<Props, State>  {

  state = { isRedirectToBlog: false };

  render() {

    const { isRedirectToBlog } = this.state;

    if (isRedirectToBlog) {
      return <Redirect to={BlogEngineSettings.BlogPostPath} push />;
    }

    const { blogInfos } = this.props;

    if (isNullOrUndefined(blogInfos)) return null;

    return blogInfos && <div style={{height: '100%', overflowY: 'auto'}}>

      {blogInfos.map( blogInfo => 
      
        <div>
        <BlogInfoComponent blogInfo={blogInfo} handleBlogInfoSelected={this.handleBlogArticleSelected.bind(this)} />
        <Spacer height = {20} />
        </div>
      )}


      </div>
      
    
  }

  handleBlogArticleSelected(id: string): void {
    
       this.props.dispatch(getBlogPost(id));
       this.setState({isRedirectToBlog: true});

  }


}

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
    return {
      dispatch
    };
}; 

const mapStateToProps = (store: IAppState) => {
  
  const state : IFunkyState = store.funkyState;
  
  return {
    blogInfos: state.blogInfos
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(BlogInfosPage);





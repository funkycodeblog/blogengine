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
import { getArticlesByTagAction } from '../../redux/Thunks';

interface Props  {

  blogInfos?: BlogInfoModel[],
 
}

interface State {
  postId : string
  tagId : string 
  isRedirectToBlog: boolean
  isRedirectToTags: boolean
}

class BlogInfosContainer extends Component<Props, State>  {

  state = { isRedirectToBlog: false, isRedirectToTags: false, postId: '', tagId: '' };

  render() {

    const { isRedirectToBlog, isRedirectToTags } = this.state;

    if (isRedirectToBlog) {
      
      const path = BlogEngineSettings.ResolveBlogPostPath(this.state.postId);
      return <Redirect to={path} push />;
    }

    if (isRedirectToTags)
    {
      const path = BlogEngineSettings.ResolveTagPath(this.state.tagId);
      return <Redirect to={path} push />;
    }

    const { blogInfos } = this.props;

    if (isNullOrUndefined(blogInfos)) return null;

    return blogInfos && <div style={{height: '100%', overflowY: 'auto', display: 'block', paddingTop: '30px'}}>

      {blogInfos.map( blogInfo => 
      
        <div key={blogInfo.id}>
        <BlogInfoComponent blogInfo={blogInfo} 
          handleBlogInfoSelected={this.handleBlogArticleSelected.bind(this)} 
          handleTagSelected={this.handleTagSelected.bind(this)} />
        <Spacer height = {20} />
        </div>
      )}

      </div>
      
    
  }

  handleBlogArticleSelected(id: string): void {
       
       this.setState({ postId: id, isRedirectToBlog: true });
  }

  handleTagSelected(tag: string) : void {

    this.setState({ tagId: tag, isRedirectToBlog: true });
  }

  


}

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
    return {
      dispatch
    };
}; 

const mapStateToProps = (store: IAppState) => {
    

};

export default connect(mapStateToProps, mapDispatchToProps)(BlogInfosContainer);





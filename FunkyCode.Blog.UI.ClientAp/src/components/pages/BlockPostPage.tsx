import React, { Component } from 'react';
import { connect } from 'react-redux';

import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { isNullOrUndefined } from 'util';
import { BlogPost } from '../../model/BlogPost';

interface Props  {
  post?: BlogPost
}

interface State {

}

class BlockPostPage extends Component<Props, State>  {

  state = { isRedirectToBlog: false };

  render() {

    const { post } = this.props;

    if (isNullOrUndefined(post))
      return null;


    // this works
    const source = post.content;
    
    if (isNullOrUndefined(source))
      return null;

    return <div style={{paddingLeft: '20px', overflowY: 'scroll', width: '750px', height: '100%'}}>
        <div dangerouslySetInnerHTML={this.createMarkup(source)} />
    </div>
    
  }

  createMarkup(content: string)
  {
    return {__html: content};

  }

  

}

const mapDispatchToProps = () => {
  return {
  
  };
};

const mapStateToProps = (store: IAppState) => {
  
  const state : IFunkyState = store.funkyState;
  
  return {
    post: state.currentPost
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(BlockPostPage);





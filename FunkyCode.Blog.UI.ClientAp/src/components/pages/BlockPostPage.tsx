import React, { Component } from 'react';
import { connect } from 'react-redux';

import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { isNullOrUndefined } from 'util';
import { BlogPost } from '../../model/BlogPost';

import Prism from "prismjs"

// @ts-ignore;
import csharp from 'prismjs/components/prism-csharp';
// @ts-ignore;
import sql from 'prismjs/components/prism-sql';
import { RouteComponentProps } from 'react-router-dom';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { getBlogPost } from '../../redux/Thunks';



interface Props extends RouteComponentProps<TParams> 
{
  post?: BlogPost;
  dispatch: ThunkDispatch<any, any, AnyAction>;
  
}

type TParams = {
  id : string;
}



interface State {

}



class BlockPostPage extends Component<Props, State>  {

  
  componentDidMount()
  {
    // this is temporary workaround for receiving 
    console.log('csharp',csharp, sql);
    console.log(this.props.match.params.id)

    const { id } = this.props.match.params;
  

    this.props.dispatch(getBlogPost(id))

  }
 

  componentDidUpdate()
  {

    console.log('Prism.highlightAll();');
    
    Prism.highlightAll();
    console.log(Prism.languages);
    
    
  }

  render() {

    const { post } = this.props;

    if (isNullOrUndefined(post))
      return null;


    // this works
    const source = post.content;
    
    if (isNullOrUndefined(source))
      return null;

    return  <div style={{height: '100%', minWidth: '500px', paddingRight: '50%'}}>
        <div dangerouslySetInnerHTML={this.createMarkup(source)}  />
    </div>
    
  }

  createMarkup(content: string)
  {
    return {__html: content};

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
    post: state.currentPost
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(BlockPostPage);





import React, { Component } from 'react';
import BlogPostPage from './BlockPostPage';
import { IAppState } from '../../redux/Store';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { getBlogPost } from '../../redux/Thunks';
import { connect } from 'react-redux';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
}

interface State  {
    
}

class AboutPage extends Component<Props, State>  {

    componentDidMount()
    {
        this.props.dispatch(getBlogPost('about-me'));
    }

    render() {
       
        return <BlogPostPage />
    }
}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
   
     };
 };
  
  const mapStateToProps = (store: IAppState) => {
    
  
  };

  export default connect(mapStateToProps, mapDispatchToProps)(AboutPage);
import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { BlogEnginePaths } from '../../config/BlogEngineSettings';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
}

interface State  {
    
}

class AboutPage extends Component<Props, State>  {

    render() {
    
        const path = BlogEnginePaths.ResolveBlogPostPath('about-me');
        return <Redirect to={ path} />
    }
}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
   
     };
 };
  
  const mapStateToProps = () => {
    return {};
  };

  export default connect(mapStateToProps, mapDispatchToProps)(AboutPage);
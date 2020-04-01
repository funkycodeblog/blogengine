import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { IAppState } from '../redux/Store';
import { MyAvatar} from '../components/UIComponents/MyAvatar'
import { Typography, List, ListItem } from '@material-ui/core';
import { BlogEnginePaths } from '../config/BlogEngineSettings';
import { Link, RouteComponentProps, withRouter } from 'react-router-dom';
import { TagBox } from './UIComponents/TagBox'
import  SearchInput  from './UIComponents/SearchInput'
import { getArticlesByTagAction } from '../redux/Thunks';
import { IFunkyState } from '../redux/State';
import { Spacer } from './Spacer'

interface Props extends RouteComponentProps  {
  
  dispatch: ThunkDispatch<any, any, AnyAction>;
  tags?: string[] ;

}

interface State  {
    
}

class NaviPanel extends Component<Props, State>  {

    render() {
        return <div style={{background: 'black', width: '100%', height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', }}>
        <div style={{height: '50px'}}/>
        
        <Link to={BlogEnginePaths.MainPath}>
          <MyAvatar size={120}/>
        </Link>

        <div style={{height: '20px'}}/>

        <Typography style = {{color: 'white'}}>Funky Code</Typography>

        <Spacer height={20} />

        <List>
              <ListItem button key='key01'>
                <Link style={{ textDecoration: 'none', color: 'white' }} to={BlogEnginePaths.AboutPath}>about me</Link>
              </ListItem>

              <ListItem button key='key02'>
                <Link style={{ textDecoration: 'none', color: 'white' }} to={BlogEnginePaths.ArchivesPath}>archives</Link>
              </ListItem>
        </List>

        <Spacer height={20} />

        <div style={{paddingLeft: '30px', paddingRight: '30px'}} >
          <TagBox type='navi' tags= {this.props.tags } tagSelected={this.tagSelected.bind(this)} />
        </div>

        <Spacer height={40} />

        <div style={{paddingLeft: '30px', paddingRight: '30px'}} >
          <SearchInput onValueEntered={this.searchEntered.bind(this)}/>
        </div>
        

        </div>
    }

    private tagSelected(tag: string) {
     
      this.props.dispatch(getArticlesByTagAction(tag));
   
    }

    private searchEntered(value: string) {
      const path = BlogEnginePaths.ResolveSearchPath(value);
      console.log(this.props);
      this.props.history.push(path);
      
    }
}

const mapStateToProps = (store: IAppState) => {

  const state: IFunkyState = store.funkyState;
  return {
    tags: state.tags
  };

};

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
    return {
      dispatch
    };
  };

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(NaviPanel));
  
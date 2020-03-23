import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { IAppState } from '../redux/Store';
import { MyAvatar} from '../components/UIComponents/MyAvatar'
import { Typography, List, ListItem } from '@material-ui/core';
import { BlogEngineSettings } from '../config/BlogEngineSettings';
import { Link } from 'react-router-dom';
import { TagBox } from './UIComponents/TagBox'
import { getArticlesByTagAction } from '../redux/Thunks';
import { IFunkyState } from '../redux/State';

interface Props {
  
  dispatch: ThunkDispatch<any, any, AnyAction>;
  tags?: string[] ;

}

interface State  {
    
}

class NaviPanel extends Component<Props, State>  {

    render() {
        return <div style={{background: 'black', width: '100%', height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', }}>
        <div style={{height: '50px'}}/>
        
        <MyAvatar size={120}/>

        <div style={{height: '20px'}}/>

        <Typography style = {{color: 'white'}}>Funky Code</Typography>

        <div style={{height: '20px'}}/>

        <List>
              <ListItem button key='key01'>
                <Link style={{ textDecoration: 'none', color: 'white' }} to={BlogEngineSettings.AboutPath}>about me</Link>
              </ListItem>

              <ListItem button key='key02'>
                <Link style={{ textDecoration: 'none', color: 'white' }} to={BlogEngineSettings.AboutPath}>archives</Link>
              </ListItem>
        </List>

        <div style={{paddingTop: '30px', paddingLeft: '30px', paddingRight: '30px'}} >
          <TagBox type='navi' tags= {this.props.tags } tagSelected={this.tagSelected.bind(this)} />
        </div>


        </div>
    }

    private tagSelected(tag: string) {
      
      this.props.dispatch(getArticlesByTagAction(tag));
    
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

export default connect(mapStateToProps, mapDispatchToProps)(NaviPanel);
  
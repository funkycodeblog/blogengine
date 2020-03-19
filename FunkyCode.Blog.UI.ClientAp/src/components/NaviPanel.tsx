import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { IAppState } from '../redux/Store';
import { MyAvatar} from '../components/UIComponents/MyAvatar'
import { Typography, List, ListItem } from '@material-ui/core';
import { BlogEngineSettings } from '../config/BlogEngineSettings';
import { Link } from 'react-router-dom';

interface Props {
 
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


        </div>
    }
}

const mapStateToProps = (store: IAppState) => {
    return {
     
    };
};

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
    return {

    };
  };

export default connect(mapStateToProps, mapDispatchToProps)(NaviPanel);
  
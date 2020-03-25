import * as React from 'react';
import '../App.css';
import { hot } from 'react-hot-loader';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import AboutPage  from './pages/AboutPage'
import { BlogEngineSettings } from '../config/BlogEngineSettings'
import BlogInfosPage from './pages/BlogInfosPage'
import BlockPostPage  from './pages/BlockPostPage';
import BlockInfosByTagPage  from './pages/BlogInfosByTag';

import ArchivesPage  from './pages/ArchivesPage';


import { connect } from 'react-redux';
import { IAppState } from '../redux/Store';
import { ErrorInfo } from '../model/ErrorInfo';
import  NaviPanel from '../components/NaviPanel';
import Progress from '../components/navigation/Progress'

interface IProps {

  errorInfo? : ErrorInfo

}

const RootApp: React.SFC<IProps> = () => {

    return ( 
    <Router>
    <div style={{height: '100%', width: '100%', display: 'block'}} >
        
        <div style={{display: 'block', position: 'fixed', left: '0px', top: '0px', width: '300px', height: '100%', background: 'red'}} >
            <NaviPanel />
        </div>

        <div style={{height: '100%', width: '100%', background: 'white', position: 'fixed', left: '300px', top: '0px', paddingLeft: '30px'}} >
          <Route exact path={BlogEngineSettings.MainPath} component={BlogInfosPage} />
          <Route path={BlogEngineSettings.AboutPath} component={AboutPage} /> 
          <Route path={BlogEngineSettings.ArchivesPath} component={ArchivesPage} /> 
          <Route path={BlogEngineSettings.BlogPostPath} component={BlockPostPage} /> 
          <Route path={BlogEngineSettings.TagPath} component={BlockInfosByTagPage} /> 
        </div>

        <Progress />
        
</div>
</Router>
        

      

     

  );
};

const mapStateToProps = (store: IAppState) => {


  return {
      errorInfo: store.funkyState.errorInfo
  };
};


const App = connect(mapStateToProps)(hot(module)(RootApp))


export default App;

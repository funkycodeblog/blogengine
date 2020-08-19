import * as React from 'react';
import '../App.css';
import { hot } from 'react-hot-loader';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import AboutPage  from './pages/AboutPage'
import { BlogEnginePaths } from '../config/BlogEngineSettings'
import BlogInfosPage from './pages/BlogInfosPage'
import BlockPostPage  from './pages/BlockPostPage';
import ContactPage  from './pages/ContactPage';
import SubscribePage  from './pages/SubscribePage';

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
    <div style={{height: '100%', width: '100%', display: 'block', position: 'fixed', top: '0px', left: '0px'}} >
        
        <div style={{float: 'left', width: '300px', height: '100%'}} >
            <NaviPanel />
        </div>

        <div style={{float: 'left', height: '100%', width: 'calc(100% - 300px)', background: 'white', overflowY: 'auto'}}>
          <div style = {{ paddingLeft: '30px', height: '100%' }} >
          <Route exact path={BlogEnginePaths.MainPath} component={BlogInfosPage} />
          <Route path={BlogEnginePaths.AboutPath} component={AboutPage} /> 
          <Route path={BlogEnginePaths.ArchivesPath} component={ArchivesPage} /> 
          <Route path={BlogEnginePaths.BlogPostPath} component={BlockPostPage} /> 
          <Route path={BlogEnginePaths.ContactPath} component={ContactPage} /> 
          <Route path={BlogEnginePaths.SubscribePath} component={SubscribePage} /> 
          </div>
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

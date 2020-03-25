import * as React from 'react';
import * as ReactDOM from 'react-dom';

/* Make the store available to all container 
components in the application without passing it explicitly */
import { Provider } from 'react-redux';

// Store type from Redux
import { Store } from 'redux';

// Import the store function and state
import configureStore, { IAppState } from './redux/Store';
import { getBlogInfos, getAllTags } from './redux/Thunks';

import './index.css';


import App from './components/App';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core';

interface IProps {
  store: Store<IAppState>;
}

const THEME = createMuiTheme({
  typography: {
   "fontFamily": "\"Zilla Slab\", \"Helvetica\", \"Arial\", sans-serif",
   "fontSize": 14,
   "fontWeightLight": 300,
   "fontWeightRegular": 400,
   "fontWeightMedium": 500
  }
});

/* 
Create a root component that receives the store via props
and wraps the App component with Provider, giving props to containers
*/
const Root: React.SFC<IProps> = props => {
  return (
    <MuiThemeProvider theme={THEME}>
    <Provider store={props.store}>
      <App />
    </Provider>
    </MuiThemeProvider>
  );
};

// Generate the store
export const funkyStore = configureStore();

funkyStore.dispatch(getAllTags());

// Render the App
ReactDOM.render(<Root store={funkyStore} />, document.getElementById(
  'root'
) as HTMLElement);


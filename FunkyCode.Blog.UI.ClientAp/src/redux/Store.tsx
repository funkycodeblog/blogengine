
import { applyMiddleware, combineReducers, createStore, Store, compose } from 'redux';
import thunk from 'redux-thunk';
import { IFunkyState } from './State';
import { funkyReducer } from './Reducer';

export interface IAppState {
  funkyState: IFunkyState;
}

const rootReducer = combineReducers<IAppState>({
  funkyState: funkyReducer,
});

export default function configureStore(): Store<IAppState, any> {
 

  if (process.env.NODE_ENV === 'development')
  {
    const composeEnhancers = (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
    const store = createStore(rootReducer, /* preloadedState, */ composeEnhancers(
      applyMiddleware(thunk)
    ));
 
    return store;
  }
  else
  {
    const store = createStore(rootReducer, undefined, applyMiddleware(thunk));
    return store;
  }

  
}
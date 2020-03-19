import { Reducer } from 'redux';
import { BlogEngineActions, FunkyActionTypes } from './ActionDefinitions';
import { IFunkyState, initialState } from './State';

export const funkyReducer: Reducer<IFunkyState, BlogEngineActions> = (
  state = initialState,
  action
) => {


  switch (action.type) {

    case FunkyActionTypes.SET_LOADING_STATUS: {

      return {
        ...state,
        isLoading: action.isLoading
      };
    }

    case FunkyActionTypes.SET_ERROR_INFO: {

      return {
        ...state,
        errorInfo: action.error,
        isMainPage: true,
        isBarVisible: true,
        isBarOptionsVisible: false,
        isInitialized: true
                
      };
    }

    case FunkyActionTypes.GET_BLOG_INFOS_SUCCESS: {

      return {
        ...state,
        blogInfos: action.blogInfos
                
      };
    }

    case FunkyActionTypes.GET_BLOG_POST_SUCCESS: {

      return {
        ...state,
        currentPost: action.blogPost
      };
    }


    default:
      return state;
  }
};

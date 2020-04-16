import { Reducer } from 'redux';
import { BlogEngineActions, FunkyActionTypes } from './ActionDefinitions';
import { IFunkyState, initialState } from './State';

export const funkyReducer: Reducer<IFunkyState, BlogEngineActions> = (
  state = initialState,
  action
) => {


  switch (action.type) {

    case FunkyActionTypes.SET_LOADING_STATUS: {

      if (action.isLoading)
      {
        return {
          ...state,
          isLoading: action.isLoading,
          currentPost: undefined
        };

      }
      else
      {
        return {
          ...state,
          isLoading: action.isLoading
        };
      }
      
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

    case FunkyActionTypes.GET_BLOG_INFOS_SUCCESS:
    case FunkyActionTypes.GET_BLOG_ARTICLES_BY_TAGS_SUCCESS:
    case FunkyActionTypes.GET_BLOG_ARTICLES_BY_SEARCH_SUCCESS:
       {
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

    case FunkyActionTypes.GET_ALL_TAGS_SUCCESS: {
      return {
        ...state,
        tags: action.tags
      };
    }

    case FunkyActionTypes.GET_ARCHIVES_SUCCESS: {
      return {
        ...state,
        archives: action.archives
      };
    }

    case FunkyActionTypes.POST_CONTACT_MESSAGE_SUCCESS: {
      return {
        ...state,
        isContactMessagePosted: true
      };
    }

    case FunkyActionTypes.RESET_UI_STATE: {
      return {
        ...state,
        isContactMessagePosted: false,
        isLoading: false,
        errorInfo: undefined
      };
    }

    default:
      return state;
  }
};

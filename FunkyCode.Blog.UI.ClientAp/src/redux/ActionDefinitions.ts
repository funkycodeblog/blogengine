
import * as Actions from './Actions';
import * as Thunks from './Thunks';

// ----------------------------------------------------------------------------------------

export enum FunkyActionTypes {
  
  SET_LOADING_STATUS = 'SET_LOADING_STATUS',
  SET_ERROR_INFO = 'SET_ERROR_INFO',
    
  GET_BLOG_INFOS = 'GET_BLOG_INFOS',
  GET_BLOG_INFOS_SUCCESS = 'GET_BLOG_INFOS_SUCCESS',

  GET_BLOG_POST = 'GET_BLOG_POST',
  GET_BLOG_POST_SUCCESS = 'GET_BLOG_POST_SUCCESS',

}

// ----------------------------------------------------------------------------------------

export type BlogEngineActions =
  | Actions.ISetLoadingStatus
  | Actions.ISetErrorInfoAction
  | Thunks.IGetBlogInfosAction
  | Thunks.IGetBlogInfosAction_Success
  | Thunks.IGetBlogPostAction
  | Thunks.IGetBlogPostAction_Success
  


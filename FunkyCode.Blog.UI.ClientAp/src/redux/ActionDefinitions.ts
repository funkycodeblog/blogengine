
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

  GET_ALL_TAGS = 'GET_ALL_TAGS',
  GET_ALL_TAGS_SUCCESS = 'GET_ALL_TAGS_SUCCESS',

  GET_BLOG_ARTICLES_BY_TAGS = 'GET_BLOG_ARTICLES_BY_TAGS',
  GET_BLOG_ARTICLES_BY_TAGS_SUCCESS = 'GET_BLOG_ARTICLES_BY_TAGS_SUCCESS',

  GET_ARCHIVES = 'GET_ARCHIVES',
  GET_ARCHIVES_SUCCESS = 'GET_ARCHIVES_SUCCESSS',

}

// ----------------------------------------------------------------------------------------

export type BlogEngineActions =
  | Actions.ISetLoadingStatus
  | Actions.ISetErrorInfoAction
  | Thunks.IGetBlogInfosAction
  | Thunks.IGetBlogInfosAction_Success
  | Thunks.IGetBlogPostAction
  | Thunks.IGetBlogPostAction_Success
  | Thunks.IGetAllTagsAction
  | Thunks.IGetAllTagsAction_Success
  | Thunks.IGetArticlesByTagAction
  | Thunks.IGetArticlesByTagAction_Success
  | Thunks.IGetArchivesAction
  | Thunks.IGetArchivesAction_Success
  
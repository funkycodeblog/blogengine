
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

  GET_BLOG_ARTICLES_BY_SEARCH = 'GET_BLOG_ARTICLES_BY_SEARCH',
  GET_BLOG_ARTICLES_BY_SEARCH_SUCCESS = 'GET_BLOG_ARTICLES_BY_SEARCH_SUCCESS',

  GET_ARCHIVES = 'GET_ARCHIVES',
  GET_ARCHIVES_SUCCESS = 'GET_ARCHIVES_SUCCESSS',

  POST_CONTACT_MESSAGE = 'POST_CONTACT_MESSAGE',
  POST_CONTACT_MESSAGE_SUCCESS = 'POST_CONTACT_MESSAGE_SUCCESS',

  RESET_UI_STATE = 'RESET_UI_STATE',

  SUBSCRIBE = 'SUBSCRIBE',
  SUBSCRIBE_SUCCESS = 'SUBSCRIBE_SUCCESS'

}

// ----------------------------------------------------------------------------------------

export type BlogEngineActions =
  | Actions.ISetLoadingStatus
  | Actions.ISetErrorInfoAction
  | Actions.IResetUiState
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
  | Thunks.IGetArticlesBySearchAction
  | Thunks.IGetArticlesBySearchAction_Success
  | Thunks.IPostContactMessage
  | Thunks.IPostContactMessage_Success
  | Thunks.ISubscribe
  | Thunks.ISubscribe_Success
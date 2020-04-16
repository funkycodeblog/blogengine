import { ActionCreator, Dispatch } from 'redux';
import { ThunkAction } from 'redux-thunk';

import iBlogService  from '../services/BlogApiHttpService';

import { IFunkyState } from './State';
import { setLoadingStatusAction, setErrorInfoAction } from './Actions';
import { FunkyActionTypes } from './ActionDefinitions';
import { ServiceResponse } from '../model/ServiceResponse';
import { ErrorInfo } from '../model/ErrorInfo';

import { BlogInfoModel } from '../model/BlogInfoModel';
import { BlogPost } from '../model/BlogPost';
import { ArchiveYearDto } from '../model/ArchiveYearDto';
import { ContactDataModel } from '../model/ContactDataModel';




// ----------------------------------------------------------------------------------------

export interface IGetBlogInfosAction {
  type: FunkyActionTypes.GET_BLOG_INFOS;
}

export interface IGetBlogInfosAction_Success {
  type: FunkyActionTypes.GET_BLOG_INFOS_SUCCESS;
  blogInfos: BlogInfoModel[];
}

export const getBlogInfos: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetBlogInfosAction_Success>
> = () => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getBlogInfosResponse = await iBlogService.GetBlogInfos();
      const isOk = checkResponseAndDispatchError(getBlogInfosResponse, dispatch);
        if (!isOk) return;

      const iGetBlogInfosActionSuccess : IGetBlogInfosAction_Success = {
        type: FunkyActionTypes.GET_BLOG_INFOS_SUCCESS,
        blogInfos: getBlogInfosResponse.Data
      }

      dispatch(iGetBlogInfosActionSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};

// ----------------------------------------------------------------------------------------

export interface IGetBlogPostAction {
  type: FunkyActionTypes.GET_BLOG_POST;
  blogPostId: string
}

export interface IGetBlogPostAction_Success {
  type: FunkyActionTypes.GET_BLOG_POST_SUCCESS;
  blogPost: BlogPost
}

export const getBlogPost: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetBlogPostAction_Success>
> = (blogPostId: string) => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getBlogPostResponse = await iBlogService.GetBlogPost(blogPostId);
      const isOk = checkResponseAndDispatchError(getBlogPostResponse, dispatch);
        if (!isOk) return;

      const getBlogPostResponseSuccess : IGetBlogPostAction_Success = {
        type: FunkyActionTypes.GET_BLOG_POST_SUCCESS,
        blogPost: getBlogPostResponse.Data
      }

      dispatch(getBlogPostResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};

// ----------------------------------------------------------------------------------------

export interface IGetAllTagsAction {
  type: FunkyActionTypes.GET_ALL_TAGS;
}

export interface IGetAllTagsAction_Success {
  type: FunkyActionTypes.GET_ALL_TAGS_SUCCESS;
  tags: string[]
}

export const getAllTags: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetAllTagsAction_Success>
> = () => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getAllTagsResponse = await iBlogService.GetAllTags();
      const isOk = checkResponseAndDispatchError(getAllTagsResponse, dispatch);
        if (!isOk) return;

      const getAllTagsResponseSuccess : IGetAllTagsAction_Success = {
        type: FunkyActionTypes.GET_ALL_TAGS_SUCCESS,
        tags: getAllTagsResponse.Data
      }

      dispatch(getAllTagsResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};

// ----------------------------------------------------------------------------------------

export interface IGetArticlesByTagAction {
  type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_TAGS;
  tag: string;
}

export interface IGetArticlesByTagAction_Success {
  type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_TAGS_SUCCESS;
  blogInfos: BlogInfoModel[]
}

export const getArticlesByTagAction: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetArticlesByTagAction_Success>
> = (tag: string) => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getPostsByTagResponse = await iBlogService.GetBlogInfosByTag(tag);
      const isOk = checkResponseAndDispatchError(getPostsByTagResponse, dispatch);
        if (!isOk) return;

      const getPostsByTagResponseSuccess : IGetArticlesByTagAction_Success = {
        type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_TAGS_SUCCESS,
        blogInfos: getPostsByTagResponse.Data
      }

      dispatch(getPostsByTagResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};

// ----------------------------------------------------------------------------------------

export interface IGetArticlesBySearchAction {
  type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_SEARCH;
  search: string;
}

export interface IGetArticlesBySearchAction_Success {
  type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_SEARCH_SUCCESS;
  blogInfos: BlogInfoModel[]
}

export const getArticlesBySearchAction: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetArticlesBySearchAction_Success>
> = (search: string) => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      console.log('getArticlesBySearchAction');
      const getPostsBySearchResponse = await iBlogService.GetBlogInfosBySearch(search);
      const isOk = checkResponseAndDispatchError(getPostsBySearchResponse, dispatch);
        if (!isOk) return;

      const getPostsBySearchResponseSuccess : IGetArticlesBySearchAction_Success = {
        type: FunkyActionTypes.GET_BLOG_ARTICLES_BY_SEARCH_SUCCESS,
        blogInfos: getPostsBySearchResponse.Data
      }

      dispatch(getPostsBySearchResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};


// ----------------------------------------------------------------------------------------

export interface IGetArchivesAction {
  type: FunkyActionTypes.GET_ARCHIVES;
}

export interface IGetArchivesAction_Success {
  type: FunkyActionTypes.GET_ARCHIVES_SUCCESS;
  archives: ArchiveYearDto[]
}

export const getArchives: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IGetArchivesAction_Success>
> = () => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getResponse = await iBlogService.GetArchives();
      const isOk = checkResponseAndDispatchError(getResponse, dispatch);
        if (!isOk) return;

      const getResponseSuccess : IGetArchivesAction_Success = {
        type: FunkyActionTypes.GET_ARCHIVES_SUCCESS,
        archives: getResponse.Data
      }

      dispatch(getResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};

// ----------------------------------------------------------------------------------------

export interface IPostContactMessage {
  type: FunkyActionTypes.POST_CONTACT_MESSAGE;
  contactMessage: ContactDataModel;
}

export interface IPostContactMessage_Success {
  type: FunkyActionTypes.POST_CONTACT_MESSAGE_SUCCESS;
}

export const postContactMessage: ActionCreator<
  ThunkAction<Promise<any>, IFunkyState, null, IPostContactMessage_Success>
> = (contactMessage: ContactDataModel) => {
  return async (dispatch: Dispatch) => {
    try {

      dispatch(setLoadingStatusAction(true));

      const getResponse = await iBlogService.PostContactMessage(contactMessage);
      const isOk = checkResponseAndDispatchError(getResponse, dispatch);
        if (!isOk) return;

      const getResponseSuccess : IPostContactMessage_Success = {
        type: FunkyActionTypes.POST_CONTACT_MESSAGE_SUCCESS,
      }

      dispatch(getResponseSuccess);

    } catch (err) {
      console.error(err);
    } finally {
      dispatch(setLoadingStatusAction(false));
    }
  };
};



// ----------------------------------------------------------------------------------------

function checkResponseAndDispatchError(response: ServiceResponse<any>, dispatch : Dispatch) : boolean
{
  
  const isError = response.Status >= 400;
  if (isError)
  {
      const errorInfo : ErrorInfo = {code: response.Status, message: response.StatusText };
      dispatch(setErrorInfoAction(errorInfo))
      return false;  
  }

  return true;
  
}

import { ActionCreator, Dispatch } from 'redux';
import { ThunkAction } from 'redux-thunk';

import iBlogService  from '../services/MockBlogService';

import { IFunkyState } from './State';
import { setLoadingStatusAction, setErrorInfoAction } from './Actions';
import { FunkyActionTypes } from './ActionDefinitions';
import { ServiceResponse } from '../model/ServiceResponse';
import { ErrorInfo } from '../model/ErrorInfo';

import { BlogInfoModel } from '../model/BlogInfoModel';
import { BlogPost } from '../model/BlogPost';




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

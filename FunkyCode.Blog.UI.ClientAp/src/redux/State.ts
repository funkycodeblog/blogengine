import { ErrorInfo } from '../model/ErrorInfo';
import { BlogInfoModel } from '../model/BlogInfoModel';
import { BlogPost } from '../model/BlogPost'

export interface IFunkyState {
  
  readonly blogInfos? : BlogInfoModel[],
  readonly currentPost?: BlogPost

  errorInfo? : ErrorInfo,
  isLoading: boolean
}

export const initialState: IFunkyState = {
  blogInfos: undefined,
  isLoading: false
};


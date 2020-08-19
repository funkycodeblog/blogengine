import { ErrorInfo } from '../model/ErrorInfo';
import { BlogInfoModel } from '../model/BlogInfoModel';
import { BlogPost } from '../model/BlogPost'
import { ArchiveYearDto } from '../model/ArchiveYearDto';
import { SubscriptionResultTypeEnum } from '../model/SubscriptionResult';

export interface IFunkyState {
  
  readonly blogInfos? : BlogInfoModel[],
  readonly currentPost?: BlogPost
  readonly tags? : string[];
  readonly archives? : ArchiveYearDto[];

  errorInfo? : ErrorInfo,
  isLoading: boolean
  isContactMessagePosted: boolean;

  subscriptionActionStatus: SubscriptionResultTypeEnum;

}

export const initialState: IFunkyState = {
  blogInfos: undefined,
  isLoading: false,
  isContactMessagePosted: false,
  subscriptionActionStatus: 'Unknown'
};


import { BlogInfoModel } from "../model/BlogInfoModel";
import { ServiceResponse } from '../model/ServiceResponse'
import { BlogPost } from "../model/BlogPost";
import { ArchiveYearDto } from "../model/ArchiveYearDto";
import { ContactDataModel } from "../model/ContactDataModel";
import { SubscribeDto } from "../model/SubscribeDto";
import { SubscriptionResultTypeEnum } from "../model/SubscriptionResult";

export interface IBlogService
{
    GetBlogInfos() : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogInfosByTag(tag: string) : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogInfosBySearch(search: string) : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogPost(id: string) : Promise<ServiceResponse<BlogPost>>
    GetAllTags() : Promise<ServiceResponse<string[]>>
    GetArchives() : Promise<ServiceResponse<ArchiveYearDto[]>>

    PostContactMessage(msgData: ContactDataModel) : Promise<ServiceResponse<void>>
    PostSubscription(subscriptionData: SubscribeDto) : Promise<ServiceResponse<SubscriptionResultTypeEnum>>
}
import { BlogInfoModel } from "../model/BlogInfoModel";
import { ServiceResponse } from '../model/ServiceResponse'
import { BlogPost } from "../model/BlogPost";

export interface IBlogService
{
    GetBlogInfos() : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogInfosByTag(tag: string) : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogPost(id: string) : Promise<ServiceResponse<BlogPost>>
    GetAllTags() : Promise<ServiceResponse<string[]>>
}
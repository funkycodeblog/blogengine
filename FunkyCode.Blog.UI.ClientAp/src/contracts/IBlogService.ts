import { BlogInfoModel } from "../model/BlogInfoModel";
import { ServiceResponse } from '../model/ServiceResponse'
import { BlogPost } from "../model/BlogPost";

export interface IBlogService
{
    GetBlogInfos() : Promise<ServiceResponse<BlogInfoModel[]>>
    GetBlogPost(id: string) : Promise<ServiceResponse<BlogPost>>
}
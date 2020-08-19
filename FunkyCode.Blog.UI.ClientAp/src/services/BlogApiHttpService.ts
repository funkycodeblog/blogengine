import { IBlogService } from "../contracts/IBlogService";
import { ServiceResponse, createServiceResponse } from "../model/ServiceResponse";
import { BlogInfoModel } from "../model/BlogInfoModel"
import { BlogPost } from "../model/BlogPost";
import axios, { AxiosResponse } from 'axios';
import IPathProvider from './PathProvider';
import { ArchiveYearDto } from "../model/ArchiveYearDto";
import { ContactDataModel } from "../model/ContactDataModel";
import { SubscribeDto } from "../model/SubscribeDto";
import { SubscriptionResultTypeEnum } from "../model/SubscriptionResult";

class BlogApiHttpService implements IBlogService
{
   
  
    async GetBlogInfos(): Promise<ServiceResponse<BlogInfoModel[]>> {

        const relativePath : string = `/api/Blog`;
        const url : string  = IPathProvider.GetApiUrl(relativePath);
       
        const axiosResponse : AxiosResponse = await axios.get(url);
    
        return createServiceResponse<BlogInfoModel[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);

    }

    async GetBlogInfosByTag(tag: string): Promise<ServiceResponse<BlogInfoModel[]>> {
    
        const url : string  = IPathProvider.GetApiUrl(`/api/Blog/Tag/${tag}`);
       
        const axiosResponse : AxiosResponse = await axios.get(url);
    
        return createServiceResponse<BlogInfoModel[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async GetBlogInfosBySearch(search: string): Promise<ServiceResponse<BlogInfoModel[]>> {
        
        const url : string  = IPathProvider.GetApiUrl(`/api/Blog/Search/${search}`);
       
        const axiosResponse : AxiosResponse = await axios.get(url);
    
        console.log('axiosResponse', axiosResponse);
        
        return createServiceResponse<BlogInfoModel[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async GetBlogPost(id: string): Promise<ServiceResponse<BlogPost>> {
        
        const relativePath : string = `/api/Blog/${id}`;
        const url : string  = IPathProvider.GetApiUrl(relativePath);
        const axiosResponse : AxiosResponse = await axios.get(url);

        return createServiceResponse<BlogPost>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async GetAllTags() : Promise<ServiceResponse<string[]>> {
        const relativePath : string = `/api/Blog/Tags`;
        const url : string  = IPathProvider.GetApiUrl(relativePath);
        const axiosResponse : AxiosResponse = await axios.get(url);
        return createServiceResponse<string[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async GetArchives(): Promise<ServiceResponse<ArchiveYearDto[]>> {
        
        const url : string  = IPathProvider.GetApiUrl(`/api/Blog/Archives`);
        const axiosResponse : AxiosResponse = await axios.get(url);
        return createServiceResponse<ArchiveYearDto[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async PostContactMessage(msgData: ContactDataModel): Promise<ServiceResponse<void>> {
        const url : string  = IPathProvider.GetApiUrl(`/api/Contact`);
        const axiosResponse : AxiosResponse = await axios.post(url, msgData);
        return createServiceResponse<void>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

    async PostSubscription(subscriptionData: SubscribeDto): Promise<ServiceResponse<SubscriptionResultTypeEnum>> {
        const url : string  = IPathProvider.GetApiUrl(`/api/Contact/Subscribe`);
        const axiosResponse : AxiosResponse = await axios.post(url, subscriptionData);
        return createServiceResponse<SubscriptionResultTypeEnum>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }
    

}

export default new BlogApiHttpService();
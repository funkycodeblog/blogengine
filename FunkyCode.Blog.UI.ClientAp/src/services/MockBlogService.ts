import { IBlogService } from "../contracts/IBlogService";
import { ServiceResponse, createServiceResponse } from "../model/ServiceResponse";
import { BlogInfoModel } from "../model/BlogInfoModel"
import { BlogPost } from "../model/BlogPost";
import axios, { AxiosResponse } from 'axios';
import IPathProvider from '../services/PathProvider';

class BlogApiHttpService implements IBlogService
{
    

    async GetBlogInfos(): Promise<ServiceResponse<BlogInfoModel[]>> {

        const relativePath : string = `/api/Blog`;
        const url : string  = IPathProvider.GetApiUrl(relativePath);
        const axiosResponse : AxiosResponse = await axios.get(url);

        return createServiceResponse<BlogInfoModel[]>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);

    }

    async GetBlogPost(id: string): Promise<ServiceResponse<BlogPost>> {
        
        const relativePath : string = `/api/Blog/${id}`;
        const url : string  = IPathProvider.GetApiUrl(relativePath);
        const axiosResponse : AxiosResponse = await axios.get(url);

        return createServiceResponse<BlogPost>(axiosResponse.data, axiosResponse.status, axiosResponse.statusText);
    }

}

export default new BlogApiHttpService();
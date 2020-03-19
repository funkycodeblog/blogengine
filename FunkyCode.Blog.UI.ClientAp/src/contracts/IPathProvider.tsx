export interface IPathProvider
{
    GetApiUrl(relativePath : string) : string;
    GetPhotoUrl(userId : string) : string;
}
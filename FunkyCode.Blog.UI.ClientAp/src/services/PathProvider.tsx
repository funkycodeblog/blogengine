import { IPathProvider } from '../contracts/IPathProvider';

class PathProvider implements IPathProvider
{
     GetApiUrl(relativePath: string): string {
        
        let root = this.getRoot();
        let path = `${root}${relativePath}`;
        return path;

    }    
    
    
    GetPhotoUrl(userId: string): string {
           
        return "";
    }

    private getRoot() : string
    {
        if (process.env.NODE_ENV === 'development')
        {
            //return 'https://skill-match.azurewebsites.net'; // this is for calling local WebApi
            return 'https://localhost:5001'; // this is for calling local WebApi
        }
        
        if (process.env.NODE_ENV === 'production')
        {
            return ''; // this is for application deployed on azure
        }
    
        return ''; 
    }
}

export default new PathProvider();
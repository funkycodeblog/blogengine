export class BlogEngineSettings {
    public static MainPath  = "/"; 
    public static AboutPath  = "/about";
    public static ArchivesPath  = "/archives";

    public static BlogPostPath  = "/post/:id";
    public static TagPath  = "/tag/:tag";
    public static SearchPath  = "/search/:search";


    public static ResolveBlogPostPath(id: string) {
        return this.BlogPostPath.replace(':id', id);
    }

    public static ResolveTagPath(tag: string) {
        return this.TagPath.replace(':tag', tag);
    }

    public static ResolveSearchPath(search: string) {
        return this.SearchPath.replace(':search', search);
    }



    public static NotFoundPath = "/404"
    public static Error = "/error"
}
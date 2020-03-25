export class BlogEngineSettings {
    public static MainPath  = "/"; 
    public static AboutPath  = "/about";
    public static ArchivesPath  = "/archives";

    public static BlogPostPath  = "/post/:id";
    public static TagPath  = "/tag/:tag";


    public static ResolveBlogPostPath(id: string) {
        return this.BlogPostPath.replace(':id', id);
    }

    public static ResolveTagPath(tag: string) {
        return this.TagPath.replace(':tag', tag);
    }



    public static NotFoundPath = "/404"
    public static Error = "/error"
}
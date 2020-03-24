import { ArchiveArticleDto } from "./ArchiveArticleDto";

export interface ArchiveYearDto
{
        year: number;
        articles: ArchiveArticleDto[];
}
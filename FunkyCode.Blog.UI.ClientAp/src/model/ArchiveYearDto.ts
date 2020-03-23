import { ArchiveArticleDto } from "./ArchiveArticleDto";

export interface ArchiveYearDto
{
        Year: number;
        Articles: ArchiveArticleDto[];
}
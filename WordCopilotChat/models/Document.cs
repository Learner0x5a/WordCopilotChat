using FreeSql.DataAnnotations;
using System;

namespace WordCopilotChat.models
{
    /// <summary>
    /// 文档信息模型
    /// </summary>
    [Table(Name = "documents")]
    public class Document
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "file_name", StringLength = 255)]
        public string FileName { get; set; }

        [Column(Name = "file_path", StringLength = 500)]
        public string FilePath { get; set; }

        [Column(Name = "file_type", StringLength = 10)]
        public string FileType { get; set; } // docx, doc, md

        [Column(Name = "file_size")]
        public long FileSize { get; set; }

        [Column(Name = "upload_time")]
        public DateTime UploadTime { get; set; }

        [Column(Name = "total_headings")]
        public int TotalHeadings { get; set; }

        [Column(Name = "is_active")]
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// 文档标题内容模型
    /// </summary>
    [Table(Name = "document_headings")]
    public class DocumentHeading
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "document_id")]
        public int DocumentId { get; set; }

        [Column(Name = "heading_text", StringLength = 500)]
        public string HeadingText { get; set; }

        [Column(Name = "heading_level")]
        public int HeadingLevel { get; set; } // 1-6

        [Column(Name = "parent_heading_id")]
        public int? ParentHeadingId { get; set; }

        [Column(Name = "content", StringLength = -1)] // 长文本
        public string Content { get; set; }

        [Column(Name = "order_index")]
        public int OrderIndex { get; set; }

        [Column(Name = "created_time")]
        public DateTime CreatedTime { get; set; }

        // 导航属性
        public Document Document { get; set; }
        public DocumentHeading ParentHeading { get; set; }
    }

    /// <summary>
    /// 文档设置模型
    /// </summary>
    [Table(Name = "document_settings")]
    public class DocumentSettings
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Column(Name = "max_documents")]
        public int MaxDocuments { get; set; } = 10;

        [Column(Name = "created_time")]
        public DateTime CreatedTime { get; set; }

        [Column(Name = "updated_time")]
        public DateTime UpdatedTime { get; set; }
    }
} 
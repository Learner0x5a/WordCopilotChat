using System;
using FreeSql.DataAnnotations;

namespace WordCopilotChat.models
{
    /// <summary>
    /// 应用程序设置模型
    /// </summary>
    [Table(Name = "app_settings")]
    public class AppSettings
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        [Column(Name = "setting_key", StringLength = 100)]
        public string SettingKey { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        [Column(Name = "setting_value", StringLength = 500)]
        public string SettingValue { get; set; }

        /// <summary>
        /// 设置描述
        /// </summary>
        [Column(Name = "description", StringLength = 200)]
        public string Description { get; set; }

        /// <summary>
        /// 数据类型 (string, int, double, bool)
        /// </summary>
        [Column(Name = "data_type", StringLength = 20)]
        public string DataType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
} 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCopilotChat.db
{
    class FreeSqlDB
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() =>
        {
            // 用户数据目录：C:\Users\<User>\.WordCopilotChat
            string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string dataRoot = Path.Combine(userHome, ".WordCopilotChat");
            try { Directory.CreateDirectory(dataRoot); } catch { }
            string dbPath = Path.Combine(dataRoot, "wc.sqlite");

            var fsql = new FreeSql.FreeSqlBuilder()
                .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))
                .UseAdoConnectionPool(true)
                // 开发环境
                //.UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=D:\projects\vsProjects\vsto\wc.sqlite;").Build();
            // 生成环境
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source={dbPath};").Build();
            return fsql;
        });
        public static IFreeSql Sqlite => sqliteLazy.Value;
    }
}

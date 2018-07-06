using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWeb
{
    public class DBHelperUtil
    {
        //静态成员变量，支持单态模式  
        private static DBHelperUtil manager = null;
        //JDBC驱动  
        private static String jdbcDriver = null;
        //主机  
        private String host = "";
        //数据库端口  
        private String port = "";
        //数据库名称  
        private String database = "";
        //数据库用户名  
        private String username = "";
        //数据库密码  
        private String password = "";

        //数据库连接字符串  
        private String connStr = "";
    }
}
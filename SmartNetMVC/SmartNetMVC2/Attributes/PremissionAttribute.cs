using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 授权许可的用户列表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowUserAttribute : Attribute
    {
        public string[] UserList { get; set; }
        /// <summary>
        /// 是否允许访问   支持3种数据类型
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public bool AllowAccess(object userIdentity)
        {
            if (userIdentity is string[])
            {
                string[] users = userIdentity as string[];
                foreach (var item in users)
                {
                    if (UserList.Contains(item))
                        return true;
                }
            }
            if (userIdentity is List<string>)
            {
                List<string> users = userIdentity as List<string>;
                foreach (var item in users)
                {
                    if (UserList.Contains(item))
                        return true;
                }
            }
            return UserList.Contains(userIdentity);
        }
    }

    /// <summary>
    /// 授权许可的角色列表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowRoleAttribute : Attribute
    {
        public string[] RoleList { get; set; }
        /// <summary>
        /// 是否允许访问
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool AllowAccess(object userRole)
        {
            if (userRole is string[])
            {
                string[] roles = userRole as string[];
                foreach (var item in roles)
                {
                    if (RoleList.Contains(item))
                        return true;
                }
            }
            if (userRole is List<string>)
            {
                List<string> roles = userRole as List<string>;
                foreach (var item in roles)
                {
                    if (RoleList.Contains(item))
                        return true;
                }
            }
            return RoleList.Contains(userRole);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smart.NetMVC2.Tool;
using Smart.NetMVC2.Const;
using System.Web;

namespace Smart.NetMVC2.PubInvoke
{
    /// <summary>
    /// 身份类
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// 新增身份
        /// </summary>
        /// <param name="authObj"></param>
        /// <returns>MD5认真ID</returns>
        public static void AddAuth(HttpContext context, object authObj, int keepSeconds = 7200) //默认保留2小时
        {
            string userIdentity = Guid.NewGuid().ToString();
            CacheHelper<object>.GetInstance().Add(userIdentity, authObj, keepSeconds);
            context.Response.SetCookie(new HttpCookie(PubConst.Client_Unique_ID, EncryptHelper.DESEnCode(userIdentity)));
        }

        /// <summary>
        /// 移除身份
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveAuth(HttpContext context)
        {
            if (context.Request.Cookies[PubConst.Client_Unique_ID] != null)
            {
                string key = context.Request.Cookies[PubConst.Client_Unique_ID].Value.ToString();
                CacheHelper<object>.GetInstance().Remove(EncryptHelper.DESDeCode(key));
                context.Response.Cookies[PubConst.Client_Unique_ID].Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// <summary>
        /// 是否存在身份
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool HasAuth(HttpContext context)
        {
            if (context.Request.Cookies[PubConst.Client_Unique_ID] != null)
            {
                string key = context.Request.Cookies[PubConst.Client_Unique_ID].Value.ToString();
                return CacheHelper<object>.GetInstance().ContainsKey(EncryptHelper.DESDeCode(key));
            }
            else
            {
                return false;
            }
        }
    }
}

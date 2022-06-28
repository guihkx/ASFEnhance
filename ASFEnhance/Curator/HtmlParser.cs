﻿#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。

using ASFEnhance.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static ASFEnhance.Utils;

namespace ASFEnhance.Curator
{
    internal static class HtmlParser
    {

        /// <summary>
        /// 解析关注的鉴赏家页
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static HashSet<CuratorItem>? ParseCuratorListPage(AjaxGetCuratorsResponse response)
        {
            if (response == null)
            {
                return null;
            }

            Match match = Regex.Match(response.Html, @"g_rgTopCurators = ([^;]+);");

            if (match.Success)
            {
                try
                {
                    string jsonStr = match.Groups[1].Value;
                    var data = JsonConvert.DeserializeObject<HashSet<CuratorItem>>(jsonStr);
                    return data;
                }
                catch (Exception ex)
                {
                    ASFLogger.LogGenericError(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
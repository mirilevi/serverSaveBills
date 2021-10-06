using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dal.Classes
{
    public static class HelpFunctions
    {
        public static string RemoveLinesContains(this string str, string pattern)
        {
            if(str =="" ||pattern ==null ||pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern,RegexOptions.IgnorePatternWhitespace);
            var results =rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                    int lineBegin = str.LastIndexOf('\n', ind);
                    int lineEnd = str.IndexOf('\n', ind + res.Length);
                    //this.StoreName = str.Substring(0, lineBegin);
                    if (lineBegin != -1 && lineEnd != -1)
                        str = str.Substring(0, lineBegin) + str.Substring(lineEnd, str.Length - lineEnd - 1);
                }
            }
            return str;
        }
        public static string RemovePattern(this string str, string pattern)
        {
            if (str == "" || pattern == null || pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var results = rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                        str = str.Substring(0, ind) + str.Substring(ind + res.Length);
                }
            }
            return str;
        }
    }
}

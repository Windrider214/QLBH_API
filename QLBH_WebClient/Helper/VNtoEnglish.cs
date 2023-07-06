using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.Helper
{
    public static class VNtoEnglish
    {
        public static String UCS2Convert(String sContent)
        {
            sContent = sContent.Trim().ToLower();
            var sUTF8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            var sUCS2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            var aUTF8Lower = sUTF8Lower.Split(new[] { '|' });

            var aUCS2Lower = sUCS2Lower.Split(new[] { '|' });

            var l = aUTF8Lower.GetUpperBound(0);

            for (var i = 1; i < l; i++)
            {
                sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);
            }
            sContent = sContent.Replace(" ", "-");
            var list = new List<string> { "/", "?", "&", ":", "#", "*", "\"", "@", "%", "$", "!", "~" };
            sContent = list.Aggregate(sContent, (current, str) => current.Replace(str, "-"));

            const string filter = "-0123456789abcdefghijklmnopqrstuvwxyz";
            var s = "";
            l = sContent.Length;
            for (var i = 0; i < l; i++)
            {
                if (filter.IndexOf(sContent[i]) >= 0)
                {
                    s = s + sContent[i];
                }
            }

            return s;
        }

    }
}
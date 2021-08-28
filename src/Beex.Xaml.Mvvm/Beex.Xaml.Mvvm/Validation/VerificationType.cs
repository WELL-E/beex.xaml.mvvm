// Copyright (c) well-e.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Beex.Xaml.Mvvm
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public enum VerificationType
    {
        [Regex]
        NULL,

        [Description("手机号码")]
        [Regex(RegexString = "^1([358][0-9]|4[579]|66|7[0135678]|9[89])[0-9]{8}$")]
        Mobile,

        [Description("电话号码(区号-电话号码-分机号)")]
        [Regex(RegexString = "^(0[0-9]{2,3}-?)?([2-9][0-9]{6,7})+(-[0-9]{1,4})?$")]
        Telephone,

        [Description("中文与字母")]
        [Regex(RegexString = "^[\u4e00-\u9fa5_a-zA-Z]+$")]
        CNChar,

        [Description("英文名")]
        [Regex(RegexString = "^[_a-zA-Z1-9]+$")]
        ENName,

        [Description("身份证号码")]
        [Regex(RegexString = @"(^\d{15}$)|(^\d{17}([0-9]|X|x)$)")]
        IDCardNo,

        [Description("数字")]
        [Regex(RegexString = "^[0-9]*$")]
        OnlyNumber,

        [Description("邮箱")]
        [Regex(RegexString = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        EMail,
    }

    public static class VerificationTypeExtension
    {
        public static List<Regex> GetRegexList(this VerificationType verificationType)
        {
            var type = verificationType.GetType();
            var field = type.GetField(verificationType.ToString());
            var attributes = field.GetCustomAttributes(typeof(RegexAttribute), false);
            return attributes.Select(a => ((RegexAttribute)a).GetRegex()).ToList();
        }
    }
}

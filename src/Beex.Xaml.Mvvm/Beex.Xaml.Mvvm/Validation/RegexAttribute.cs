// Copyright (c) well-e.
// Licensed under the MIT License.

using System;
using System.Text.RegularExpressions;

namespace Beex.Xaml.Mvvm
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class RegexAttribute : Attribute
    {
        /// <summary>
        /// 正则表达式校验字符串
        /// </summary>
        public string RegexString { get; set; }

        public RegexAttribute() { }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        /// <returns></returns>
        public Regex GetRegex()
        {
            return string.IsNullOrEmpty(RegexString) ? null : new Regex(RegexString);
        }
    }
}

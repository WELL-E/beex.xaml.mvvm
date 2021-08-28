// Copyright (c) well-e.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;

namespace Beex.Xaml.Mvvm
{
    // <summary>
    /// 属性校验规则
    /// </summary>
    public class PropertyValidationRule : ValidationRule
    {
        [Description("属性名称")]
        public string PropertyName { get; set; }

        [Description("是否必须")]
        public bool IsRequired { get; set; }

        [Description("最小长度")]
        public uint MinLength { get; set; }

        [Description("最大长度")]
        public uint? MaxLength { get; set; }

        [Description("校验类型")]
        public VerificationType VerificationType { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (IsRequired)
            {
                if (value == null)
                {
                    return new ValidationResult(false, $"{PropertyName}不能为空");
                }
            }

            if (!(value is string s)) return new ValidationResult(true, null);
            if (IsRequired && (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)))
            {
                return new ValidationResult(false, $"{PropertyName}不能为空");
            }

            if (string.IsNullOrEmpty(s)) return new ValidationResult(true, null);
            if (s.Length < MinLength)
            {
                return new ValidationResult(false, $"{PropertyName}长度最少为{MinLength}");
            }
            if (MaxLength.HasValue)
            {
                if (s.Length > MaxLength)
                {
                    return new ValidationResult(false, $"{PropertyName}长度最大为{MaxLength}");
                }
            }

            var regexList = VerificationType.GetRegexList();
            foreach (var regex in regexList)
            {
                if (regex != null && !regex.IsMatch(s))
                {
                    return new ValidationResult(false, $"{PropertyName}格式错误");
                }
            }
            return new ValidationResult(true, null);
        }
    }

   

  

   
}

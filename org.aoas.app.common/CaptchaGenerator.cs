
/*
 * guid: $GUID$
 * file: CaptchaGenerator
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/20 10:53:56
 * desc: 验证码生成处理模块
 ************************************
 *
 * upgrade history:
 ************************************
 * author: 
 * update: 
 * ver-desc:
 * 
 */

namespace org.aoas.app.common
{
    using System;

    /// <summary>
    /// 验证码生成处理模块
    /// </summary>
    public sealed class CaptchaGenerator
    {
        /// <summary>
        /// 上下文内容同步锁
        /// </summary>
        private readonly static object _SyncLocker = new object();

        /// <summary>
        /// 单实例对象
        /// </summary>
        private static CaptchaGenerator _singleInstance;

        /// <summary>
        /// 验证码可允许的数字字符
        /// </summary>
        private static readonly char[] _Numbers = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        /// <summary>
        /// 验证码可允许的字符
        /// </summary>
        private static readonly char[] _Characters = {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        /// <summary>
        /// 验证码可允许的操作符
        /// </summary>
        private static readonly char[] _Operations = {
            '+', '-', '*', '/'
        };

        /// <summary>
        /// 验证码最小长度
        /// </summary>
        private readonly int _minSize;

        /// <summary>
        /// 验证码最大长度
        /// </summary>
        private readonly int _maxSize;

        /// <summary>
        /// 创建 <see cref="CaptchaGenerator"/> 类型的新实例
        /// </summary>
        /// <param name="minSize">验证码最小可允许长度</param>
        /// <param name="maxSize">验证码最大可允许长度</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CaptchaGenerator(int minSize =4, int maxSize = 6)
        {
            minSize.ThrowIfLessThenOrEqual<int>(0);
            maxSize.ThrowIfLessThenOrEqual<int>(0);
            minSize.ThrowIfLessThen<int>(maxSize);

            _minSize = minSize;
            _maxSize = maxSize;
        }

        /// <summary>
        /// 生成数字验证码，返回生成的验证码
        /// </summary>
        /// <param name="size">当前验证码长度，默认值为 4</param>
        /// <returns></returns>
        public string GenerateNumberCaptcha(int size = 4)
        {
            size.ThrowIfLessThen<int>(_minSize);
            size.ThrowIfMoreThen<int>(_maxSize);

            var arr = new char[size];
            var rdn = new Random();
            for (int i = 0; i < size; i++)
            {
                var idx = rdn.Next(0, _Numbers.Length - 1);
                arr[i] = _Numbers[idx];
            }

            return string.Concat(arr);
        }
                
        /// <summary>
        /// 验证码默认生成器
        /// </summary>
        public static CaptchaGenerator Default
        {
            get
            {
                if(_singleInstance.IsNull())
                {
                    lock (_SyncLocker)
                    {
                        if (_singleInstance.IsNull())
                        {
                            _singleInstance = new CaptchaGenerator();
                        }
                    }
                }

                return _singleInstance;
            }
        }
    }
}

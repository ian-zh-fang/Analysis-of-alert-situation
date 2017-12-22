
/*
 * guid: $GUID$
 * file: CachePolicy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 15:50:48
 * desc: 
 ************************************
 *
 * upgrade history:
 ************************************
 * author: 
 * update: 
 * ver-desc:
 * 
 */

namespace org.aoas.cache
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 旨在实现缓存策略的一组基本业务功能
    /// </summary>
    public abstract class CachePolicy : DisposeDependancy, ICachePolicy, IDisposable
    {
        /// <summary>
        /// 缓存失效事件
        /// </summary>
        private event Action _onInvalid;

        /// <summary>
        /// 缓存上下文
        /// </summary>
        private ICacheContext _context;

        /// <summary>
        /// 创建 <see cref="CachePolicy"/> 类型的新实例，
        /// </summary>
        protected CachePolicy() { }

        event Action ICachePolicy.OnInvalid
        {
            add
            {
                if (value.IsNull()) { return; }
                _onInvalid += value;
            }

            remove
            {
                if (value.IsNull()) { return; }
                _onInvalid -= value;
            }
        }

        ICacheContext ICachePolicy.Context
        {
            get { return _context; }
            set
            {
                if (value.IsNull()) { return; }

                // 先解除之前的事件回调
                if (!_context.IsNull())
                {
                    _context.OnChange -= OnChangeCallback;
                    _context.OnInit -= OnInitCallback;
                }
                _context = value;

                // 重新设置新的事件回调
                _context.OnInit += OnInitCallback;
                _context.OnChange += OnChangeCallback;
            }
        }

        bool ICachePolicy.Check()
        {
            var sta = OnCheck();
            if (sta) { OnInvalidInvokeAsync(); }

            return sta;
        }

        /// <summary>
        /// 缓存上下文初始化时，事件回调处理函数
        /// </summary>
        private void OnInitCallback()
        {
            ((ICachePolicy)this).Check();
        }

        /// <summary>
        /// 缓存发生改变时，事件回调处理函数
        /// </summary>
        protected virtual void OnChangeCallback()
        {
            OnInvalidInvokeAsync();
        }

        /// <summary>
        /// 并行发生缓存失效事件 <see cref="ICachePolicy.OnInvalid"/>
        /// </summary>
        protected void OnInvalidInvokeAsync()
        {
            if (_onInvalid.IsNull()) { return; }
            Parallel.Invoke(_onInvalid);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
            _onInvalid = null;
        }

        /// <summary>
        /// 校验缓存是否失效，若缓存失效，返回 true；否则，返回 false .
        /// 由具体的缓存策略具体实现 .
        /// </summary>
        /// <returns></returns>
        protected abstract Boolean OnCheck();
    }
}

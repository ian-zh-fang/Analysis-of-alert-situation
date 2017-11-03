
/*
 * guid: $GUID$
 * file: DisposeDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:07:22
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

namespace org.aoas
{
    using System;

    /// <summary>
    /// 支持实现资源释放的依赖
    /// </summary>
    public abstract class DisposeDependancy : IDisposable
    {
        private Boolean _isDisposed;

        /// <summary>
        /// 一种受保护机制的构造函数
        /// </summary>
        protected DisposeDependancy()
        {
            _isDisposed = false;
        }

        // 释放占用的内部资源
        //  disposing：非托管资源释放状态。true 标识释放非托管资源；否则，不释放非托管资源
        private void Disposed(bool disposing)
        {
            if (_isDisposed) return;
            Dispose(disposing);
            _isDisposed = true;
        }

        /// <summary>
        /// 释放占用的内部资源，包括托管和非托管资源
        /// </summary>
        /// <param name="disposing">非托管资源释放状态。true 标识释放非托管资源；否则，不释放非托管资源</param>
        protected virtual void Dispose(bool disposing) { }

        /// <summary>
        /// 释放占用的内部资源，包括托管和非托管资源
        /// </summary>
        public void Dispose()
        {
            Disposed(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 资源是否已经释放。true 标识资源已释放。否则，资源尚未释放
        /// </summary>
        public Boolean IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// 析构函数，这是 Dispose 函数的补充，此时不会释放非托管资源
        /// </summary>
        ~DisposeDependancy()
        {
            Disposed(false);
        }
    }
}

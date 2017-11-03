
/*
 * guid: $GUID$
 * file: ExecutableDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:13:31
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
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 可执行程序的依赖。
    /// 定义了一种需要执行内核处理的基础业务逻辑结构，
    /// 包含内核处理前后操作，安全的异常处理，以及相关资源释放。
    /// </summary>
    public abstract class ExecutableDependancy
        : DisposeDependancy
        , IDisposable
    {
        private bool _kernalRunning;

        // 一组异常处理特性
        private attributes.ExceptionHandleAttribute[] _exceptionHandleAttributes;

        // 内核执行中断上下文对象，通常用来中断执行内核
        private InterruptContext _interrupt;

        /// <summary>
        /// 一种受保护机制的构造函数
        /// </summary>
        protected ExecutableDependancy()
            : base()
        {
            Initialize();
        }

        // 内部对象初始化过程
        protected virtual void Initialize()
        {
            _kernalRunning = false;
            InitExceptionHandle();
        }

        // 初始化异常处理特性，返回一组异常处理特性对象
        private attributes.ExceptionHandleAttribute[] InitExceptionHandle()
        {
            attributes.ExceptionHandleAttribute[] attrs;
            try
            {
                attrs = GetType().GetCustomAttributes<attributes.ExceptionHandleAttribute>().ToArray();
            }
            catch (Exception) { attrs = new attributes.ExceptionHandleAttribute[0]; }

            return _exceptionHandleAttributes = attrs;
        }

        /// <summary>
        /// 开始执行内核
        /// </summary>
        protected void BeginExecute()
        {
            // 初始化内核执行上下文
            InitExecuteContext();

            // 执行内核过程
            ExecutedContext ctx = OnExecute();

            try
            {
                // 内核过程执行完成之后操作
                OnExecuted(ctx);
            }
            catch (Exception err) { OnExceptionHandle(err); }

            // 释放内核执行上下文
            ReleaseExecuteContext();
            _kernalRunning = false;
        }

        // 初始化内核中断执行上下文对象
        private InterruptContext InitExecuteContext()
        {
            _kernalRunning = true;
            if (_interrupt.IsNull()) { return _interrupt = new InterruptContext(); }
            if (_interrupt.IsDisposed) { return _interrupt = new InterruptContext(); }
            if (_interrupt.IsInvalid) { return _interrupt = new InterruptContext(); }

            return _interrupt;
        }

        // 释放当前上下文中的内核中断上下文对象
        private void ReleaseExecuteContext()
        {
            if (_interrupt.IsNull()) { return; }
            if (_interrupt.IsCancel) { return; }

            try
            {
                OnDisposed(_interrupt.FromDispose);
            }
            catch (Exception err) { OnExceptionHandle(err); }
        }

        // 中断执行内核过程
        //  interruptFrom：中断信号源。true 标识正常取消；否则，标识有资源释放导致中断内核执行，默认值为 true。
        private void InterruptExecute(bool interruptFrom = true, bool fromDispose = false)
        {
            if (IsDisposed) { return; }
            if (_interrupt.IsNull()) { return; }
            if (_interrupt.IsDisposed) { return; }

            _interrupt.Invoke(interruptFrom, fromDispose);

            // 当前中断操作是由正常取消导致的，停止执行其他业务
            if (interruptFrom) { return; }

            // 当前中断操作是由非正常取消导致的
            // 内核运行状态处于执行中，停止执行其他业务，委托由其他业务执行
            if (_kernalRunning) { return; }
            // 内核运行结束，执行资源回收操作
            ReleaseExecuteContext();
        }

        /// <summary>
        /// 结束执行内核
        /// </summary>
        protected void EndExecute()
        {
            InterruptExecute(true);
        }

        // 执行内核业务过程，并返回执行
        private ExecutedContext OnExecute()
        {
            ExecutedContext ctx;
            try
            {
                if (IsDisposed) { throw new ObjectDisposedException(GetType().FullName); }

                OnExecuting();
                ctx = ExecuteCore(_interrupt.Cancellation);
            }
            catch (Exception err) { ctx = new ExecutedContext { Error = err }; }

            return ctx;
        }

        /// <summary>
        /// 内核执行前操作过程
        /// </summary>
        protected virtual void OnExecuting() { }

        /// <summary>
        /// 内核执行后操作过程
        /// </summary>
        /// <param name="context">内核执行完成上下文</param>
        protected virtual void OnExecuted(ExecutedContext context)
        {
            if (context.IsNull()) { return; }
            if (context.Error.IsNull()) { return; }
            OnExceptionHandle(context.Error);
        }

        // 使用指定的异常处理特性处理内部异常过程。成功，返回 true；否则，返回 false。
        //  error：异常错误信息。
        //  owner：生产异常错误的对象。
        private bool OnExceptionHandleAttributeInvoke(attributes.ExceptionHandleAttribute handle, Exception error, object owner)
        {
            if (handle.IsNull()) { return false; }

            bool sta;
            try
            {
                sta = handle.Invoke(error, owner);
            }
            catch (Exception err)
            {
                sta = false;
                OnException(err, handle, false);
            };

            return sta;
        }

        // 异常处理特性处理内部异常过程。成功，返回 true；否则，返回 false。
        //  error：异常错误信息。
        //  owner：生产异常错误的对象。
        private bool OnExceptionHandleAttribute(attributes.ExceptionHandleAttribute[] handles, Exception error, object owner)
        {
            if (handles.IsNull()) { return false; }

            bool isOk;
            try
            {
                isOk =
                    handles.Select(t => OnExceptionHandleAttributeInvoke(t, error, owner)).ToArray() // 遍历所有的异常处理
                    .Any(); // 确定是否有处理完成的异常处理过程
            }
            catch (Exception err)
            {
                OnException(err, this, false);
                isOk = false;
            }

            return isOk;
        }

        // 内部异常处理过程
        //  error：异常错误信息。
        //  owner：生产异常错误的对象，若不指定，标识当前上下文中 this 指向的对象
        protected void OnExceptionHandle(Exception error, object owner = null)
        {
            owner = owner ?? this;
            var isOk = OnExceptionHandleAttribute(_exceptionHandleAttributes, error, owner);
            OnException(error, owner, isOk);
        }

        // 内部异常处理。
        //  error：异常错误信息。
        //  owner：生产异常错误的对象。
        //  isHandled：当前异常错误是否已经处理。true 标识已处理当前异常错误，否则，标识尚未处理。
        protected virtual void OnException(Exception error, object owner, bool isHandled = false)
        {
            if (error.IsNull()) { return; }
            if (isHandled) { return; }
            throw error;
        }

        /// <summary>
        /// 资源释放处理过程，在释放资源时，若没有终结内核执行过程，在内核线程中断之后，执行但当前过程
        /// </summary>
        /// <param name="disposing">是否释放非托管资源，如数据库连接，COM 对象，MDI+ 对象等等</param>
        protected virtual void OnDisposed(bool disposing)
        {
            ReleaseExceptionHandle();
            ReleaseInterrupt();
        }

        // 释放内核中断执行上下文
        private void ReleaseInterrupt()
        {
            if (_interrupt.IsNull()) { return; }
            if (_interrupt.IsDisposed) { return; }

            _interrupt.Dispose();
            _interrupt = null;
        }

        // 释放异常处理特性对象
        private void ReleaseExceptionHandle()
        {
            if (_exceptionHandleAttributes.IsNull()) { return; }
            Array.Clear(_exceptionHandleAttributes, 0, _exceptionHandleAttributes.Length);
            _exceptionHandleAttributes = null;
        }

        // 定义资源释放业务过程
        protected sealed override void Dispose(bool disposing)
        {
            // 释放其他内部资源
            base.Dispose(disposing);

            // 中断内核执行状态
            InterruptExecute(false, disposing);
        }

        /// <summary>
        /// 内核执行过程，返回内核执行完成状态上下文
        /// </summary>
        /// <param name="cancellation">内核取消操作信号接收对象</param>
        /// <returns></returns>
        protected abstract ExecutedContext ExecuteCore(CancellationProvider cancellation);

        /// <summary>
        /// 内核执行完成上下文
        /// </summary>
        protected class ExecutedContext
        {
            /// <summary>
            /// 内核执行过程中发生的错误信息
            /// </summary>
            public Exception Error { get; set; }
        }

        /// <summary>
        /// 执行中断上下文
        /// </summary>
        private class InterruptContext
            : DisposeDependancy
        {
            // 内部状态同步对象
            private readonly object _syncLocker = new object();

            // 内部有效状态，当前值为 true 时，标识当前上下文状态无效。
            private bool _sta;

            // 内部取消操作广播
            private CancellationProvider _cancellation;

            /// <summary>
            /// 创建 InterruptContext 的新实例，并返回创建的对象
            /// </summary>
            /// <param name="defaultCancel">是否正常取消默认状态。true 标识正常取消；否则，标识异常取消</param>
            public InterruptContext(bool defaultCancel = true)
                : base()
            {
                _cancellation = new CancellationProvider();
                _sta = false;
                IsCancel = defaultCancel;
            }

            /// <summary>
            /// 中断内核执行过程
            /// </summary>
            /// <param name="cancel">中断操作状态。true 标识正常取消；否则，标识异常取消</param>
            public void Invoke(bool cancel = true, bool fromDispose = false)
            {
                IsCancel = cancel;
                FromDispose = fromDispose;

                if (_sta) { return; }
                lock (_syncLocker)
                {
                    if (_sta) { return; }
                    _sta = true;

                    if (_cancellation.IsNull()) { return; }
                    _cancellation.Cancel();
                }
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                ReleaseCancellation();
            }

            // 释放取消资源
            private void ReleaseCancellation()
            {
                if (_cancellation.IsNull()) { return; }
                if (_cancellation.IsDisposed) { return; }
                _cancellation.Dispose();
                _cancellation = null;
            }

            /// <summary>
            /// 资源释放状态
            /// </summary>
            public bool FromDispose { get; private set; }

            /// <summary>
            /// 是否正常取消。true 标识正常取消；否则，标识异常取消
            /// </summary>
            public bool IsCancel { get; private set; }

            /// <summary>
            /// 当前状态是否无效。true 标识当前上下文无效；否则，有效
            /// </summary>
            public bool IsInvalid
            {
                get { return _sta; }
            }

            /// <summary>
            /// 取消操作
            /// </summary>
            public CancellationProvider Cancellation
            {
                get { return _cancellation; }
            }
        }
    }
}

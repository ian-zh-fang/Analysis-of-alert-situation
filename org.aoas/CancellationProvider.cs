
/*
 * guid: $GUID$
 * file: CancellationProvider
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:08:56
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
    using System.Threading;
    using System.Threading.Tasks;


    // 一种安全的取消机制驱动器
    public sealed class CancellationProvider
        : DisposeDependancy
        , IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Boolean _isCancelled;

        public CancellationProvider()
            : this(new CancellationTokenSource())
        { }

        public CancellationProvider(int milliseconds)
            : this(new CancellationTokenSource(milliseconds))
        { }

        public CancellationProvider(TimeSpan delay)
           : this(new CancellationTokenSource(delay))
        { }

        // 基于 CancellationTokenSource 实例化一个 CancellationProvider 的对象
        private CancellationProvider(CancellationTokenSource cancellation)
            : base()
        {
            _cancellationTokenSource = cancellation;
            _isCancelled = false;
        }

        /// <summary>
        /// 传达取消请求。
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="AggregateException"></exception>
        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        // 传达对取消的请求，并指定是否应处理其余回调和可取消操作。
        //   throwOnFirstException:
        //     如果可以立即传播异常，则为 true；否则为 false。
        public void Cancel(Boolean throwOnFirstException)
        {
            _cancellationTokenSource.Cancel(throwOnFirstException);
        }

        // 在指定的毫秒数后计划对此 System.Threading.CancellationTokenSource 的取消操作。
        //   milliseconds:
        //     取消此 System.Threading.CancellationTokenSource 前等待的时间范围。
        public void CancelAfter(int milliseconds)
        {
            _cancellationTokenSource.CancelAfter(milliseconds);
        }

        // 在指定的时间跨度后计划对此 System.Threading.CancellationTokenSource 的取消操作。
        //   delay:
        //     取消此 System.Threading.CancellationTokenSource 前等待的时间范围。
        public void CancelAfter(TimeSpan delay)
        {
            _cancellationTokenSource.CancelAfter(delay);
        }

        /// <summary>
        /// 等待 System.Threading.Tasks.Task 在指定的毫秒数内完成执行。
        /// </summary>
        /// <param name="millisecondsTimeout">等待的毫秒数，或为 System.Threading.Timeout.Infinite (-1)，表示无限期等待。</param>
        /// <exception cref="System.OperationCanceledException">操作被取消</exception>
        public void Wait(int millisecondsTimeout)
        {
            try
            {
                Task.Delay(millisecondsTimeout, Token).Wait();
            }
            catch (Exception) { throw new OperationCanceledException("Operation has cancelled .", Token); }
        }

        // 获取与此 System.Threading.CancellationToken 关联的 System.Threading.CancellationTokenSource。
        // 异常:
        //   T:System.ObjectDisposedException:
        //     标记源已被释放。
        public CancellationToken Token
        {
            get { return _cancellationTokenSource.Token; }
        }

        // 取消状态信息。以下情况均被视为已取消状态
        //  R1，取消信号到达
        //  R2，取消信号相关资源被释放
        public Boolean IsCancelled
        {
            get
            {
                if (_isCancelled) return _isCancelled;

                try
                {
                    _isCancelled = _cancellationTokenSource.IsCancellationRequested;
                }
                catch (OperationCanceledException) { _isCancelled = true; }
                catch (ObjectDisposedException) { _isCancelled = true; }
                catch (Exception) { }

                return _isCancelled;
            }
        }
    }
}

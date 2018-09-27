using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace BankBinCodeChecker.Helpers
{
    public class AsyncHelpers
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        public static T RunSync<T>(Func<Task<T>> func)
        {           
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew<Task<T>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap<T>().GetAwaiter().GetResult();
        }
    }
}
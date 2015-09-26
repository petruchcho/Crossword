using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    class AsyncTask<TResult>
    {
        public delegate TResult AsyncMethod();

        private Func<TResult> method;
        private Action<TResult> callback;

        private TResult result;

        public AsyncTask(Func<TResult> method, Action<TResult> callback)
        {
            this.method = method;
            this.callback = callback;
        }

        public async void Start()
        {
            await Task.Run(() =>
            {
                result = method.Invoke();
            });
            callback.Invoke(result);
        }
    }
}

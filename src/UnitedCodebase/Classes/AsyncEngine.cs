using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace UnitedCodebase.Classes
{
    public class AsyncEngine
    {
        private static string _result;

        public string strResult
        {
            get
            {
                return _result;
            }
        }

        public static async void Execute(IAsyncAction action)
        {
            await action;
            if (action.Status == AsyncStatus.Completed)
            {
                action.Close();
                action = null;
                GC.Collect(1, GCCollectionMode.Forced);
            }
        }

        public static async void ExecuteString(IAsyncOperation<string> action)
        {
            _result = await action;
            if (action.Status == AsyncStatus.Completed)
            {
                action.Close();
                action = null;
                GC.Collect(1, GCCollectionMode.Forced);
            }
        }
    }
}

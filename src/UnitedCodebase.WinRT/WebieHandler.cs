using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml.Input;

namespace UnitedCodebase.WinRT
{
    public delegate void NotifyAppHandler(string e);
    public delegate void ShowContextMenuEventHandler(int x, int y);

    [AllowForWeb]
    public sealed class WebieHandler
    {
        public event NotifyAppHandler NotifyAppEvent;

        public event ShowContextMenuEventHandler ShowContextMenuEvent;

        public async void sendData(string keyPress)
        {
            var window = CoreWindow.GetForCurrentThread();
            await Task.Run(async () =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => 
                {
                    OnNotifyApp(keyPress);
                });
            });
        }

        public async void showContextMenu(int x, int y)
        {
            var window = CoreWindow.GetForCurrentThread();
            await Task.Run(async () =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    OnShowContextMenu(x, y);
                });
            });
        }

        private void OnShowContextMenu(int x, int y)
        {
            var completedEvent = ShowContextMenuEvent;
            if (completedEvent != null)
            {
                completedEvent(x, y);
                GC.Collect(1, GCCollectionMode.Forced);
            }
        }

        private void OnNotifyApp(string args)
        {
            var completedEvent = NotifyAppEvent;
            if (completedEvent != null)
            {
                completedEvent(args);
                GC.Collect(1, GCCollectionMode.Forced);
            }
        }
    }
}

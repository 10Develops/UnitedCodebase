using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Devices.Lights;
using Windows.Devices.Enumeration;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Diagnostics;
using Windows.Foundation;

namespace UnitedCodebase.Classes
{
    public class UCNotification
    {
        public string ToastButtonContent;
        public string ToastButtonArguments;
        public string ToastButton2Content;
        public string ToastButton2Arguments;
        public string ToastLaunchArguments;
        public string Title;
        public string Content;

        public UCNotification(string _title, string _content)
        {
            Title = _title;
            Content = _content;
        }

        public void ShowNotification()
        {
            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                        {
                            new AdaptiveText()
                            {
                                Text = Title
                            },

                            new AdaptiveText()
                            {
                                Text = Content
                            },
                        },
                }
            };

            // Now we can construct the final toast content
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual
            };

            if (ToastLaunchArguments != null)
            {
                toastContent.Launch = ToastLaunchArguments;
            }

            ToastActionsCustom actionsCustom = new ToastActionsCustom();

            if (ToastButtonContent != null && ToastButtonArguments != null)
            {
                actionsCustom.Buttons.Add(new ToastButton(ToastButtonContent, ToastButtonArguments)
                {
                    ActivationType = ToastActivationType.Foreground
                });
            }

            if (ToastButton2Content != null && ToastButton2Arguments != null)
            {
                actionsCustom.Buttons.Add(new ToastButton(ToastButton2Content, ToastButton2Arguments)
                {
                    ActivationType = ToastActivationType.Foreground
                });
            }

            toastContent.Actions = actionsCustom;

            // And create the toast notification
            ToastNotification toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}


﻿using MoneyFox.Shared.Resources;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Plugins.Email;
using MvvmCross.Plugins.WebBrowser;
using Npadrutt.MvxPlugins.AppInformation;
using Npadrutt.MvxPlugins.StoreOperations;

namespace MoneyFox.Shared.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAppInformation appInformation;
        private readonly IMvxComposeEmailTask composeEmailTask;
        private readonly IStoreOperations storeFeatures;
        private readonly IMvxWebBrowserTask webBrowserTask;

        /// <summary>
        ///     Creates an AboutViewModel Object
        /// </summary>
        /// <param name="appInformation">Instance of a <see cref="IAppInformation" /> implementation.</param>
        /// <param name="composeEmailTask">Instance of a <see cref="IMvxComposeEmailTask" /> implementation.</param>
        /// <param name="webBrowserTask">Instance of a <see cref="IMvxWebBrowserTask" /> implementation.</param>
        /// <param name="storeOperations">Instance of a <see cref="IStoreOperations" /> implementation.</param>
        public AboutViewModel(IAppInformation appInformation,
            IMvxComposeEmailTask composeEmailTask,
            IMvxWebBrowserTask webBrowserTask,
            IStoreOperations storeOperations)
        {
            this.appInformation = appInformation;
            this.composeEmailTask = composeEmailTask;
            this.webBrowserTask = webBrowserTask;
            storeFeatures = storeOperations;
        }

        /// <summary>
        ///     Opens the webbrowser and loads to the apply solutions
        ///     website
        /// </summary>
        public MvxCommand GoToWebsiteCommand => new MvxCommand(GoToWebsite);

        /// <summary>
        ///     Sends a feedback mail to the apply solutions support
        ///     mail address
        /// </summary>
        public MvxCommand SendMailCommand => new MvxCommand(SendMail);

        /// <summary>
        ///     Opens the store to rate the app.
        /// </summary>
        public MvxCommand RateAppCommand => new MvxCommand(RateApp);

        /// <summary>
        ///     Opens the webbrowser and loads repository page
        ///     on GitHub
        /// </summary>
        public MvxCommand GoToRepositoryCommand => new MvxCommand(GoToRepository);

        /// <summary>
        ///     Opens the webbrowser and loads the project on crowdin.
        /// </summary>
        public MvxCommand GoToTranslationProjectCommand => new MvxCommand(GoToTranslationProject);

        /// <summary>
        ///     Opens the webbrowser and loads the project on crowdin.
        /// </summary>
        public MvxCommand GoToDesignerTwitterAccountCommand => new MvxCommand(GoToDesignerTwitterAccount);        
        
        /// <summary>
        ///     Opens the webbrowser loads the contribution page on Github.
        /// </summary>
        public MvxCommand GoToContributionPageCommand => new MvxCommand(GoToContributionPage);

        /// <summary>
        ///     Provides an TextSource for the translation binding on this page.
        /// </summary>
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name);

        /// <summary>
        ///     Returns the Version of App
        /// </summary>
        public string Version => appInformation.GetVersion();

        /// <summary>
        ///     Returns the apply solutions webite url from the
        ///     ressource file
        /// </summary>
        public string Website => Constants.Constants.WEBSITE_URL;

        /// <summary>
        ///     Returns the mailaddress for support cases from the
        ///     ressource file
        /// </summary>
        public string SupportMail => Constants.Constants.SUPPORT_MAIL;

        private void GoToWebsite()
        {
            webBrowserTask.ShowWebPage(Constants.Constants.WEBSITE_URL);
        }

        private void SendMail()
        {
            composeEmailTask.ComposeEmail(Constants.Constants.SUPPORT_MAIL,
                string.Empty,
                Strings.FeedbackSubject,
                string.Empty,
                true);
        }

        private void RateApp()
        {
            storeFeatures.RateApp();
        }

        private void GoToRepository()
        {
            webBrowserTask.ShowWebPage(Constants.Constants.GIT_HUB_REPOSITORY_URL);
        }

        private void GoToTranslationProject()
        {
            webBrowserTask.ShowWebPage(Constants.Constants.TRANSLATION_PROJECT_URL);
        }
        
        private void GoToDesignerTwitterAccount() {
            webBrowserTask.ShowWebPage(Constants.Constants.ICONDESIGNER_TWITTER_URL);
        }

        private void GoToContributionPage() {
            webBrowserTask.ShowWebPage(Constants.Constants.ICONDESIGNER_TWITTER_URL);
        }
    }
}
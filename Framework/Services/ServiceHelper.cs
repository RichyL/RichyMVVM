using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichyMVVM.Framework.Services
{
    /// <summary>
    /// A class used to obtain a reference to the IoC Container in a platform independent mannner.
    /// </summary>
    /// <remarks>This class is from https://twitter.com/davidortinau</remarks>
    public static class ServiceHelper
    {
        /// <summary>
        /// Get an instance of a type using generics
        /// </summary>
        /// <typeparam name="TService">Generic type to return</typeparam>
        /// <returns>A transient or singleton reference to the specified type</returns>
        public static TService GetService<TService>() => Current.GetService<TService>();


        /// <summary>
        /// Get an instance of type using a Type variable
        /// </summary>
        /// <param name="serviceType">Specifies the Type to return</param>
        /// <returns>A transient or singleton reference to the specified type</returns>
        public static object GetService(Type serviceType) => Current.GetService(serviceType);

        /// <summary>
        /// Get a reference to the current service provider in a platform agnostic manner
        /// </summary>
        public static IServiceProvider Current =>
        #if WINDOWS10_0_17763_0_OR_GREATER
            MauiWinUIApplication.Current.Services;
        #elif ANDROID
                MauiApplication.Current.Services;
        #elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
        #else
			null;
        #endif
    }
}

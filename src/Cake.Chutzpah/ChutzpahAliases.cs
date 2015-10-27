using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Chutzpah
{
    /// <summary>
    ///     Contains functionality related to running javascript unit tests using Chutzpah.
    /// </summary>
    [CakeAliasCategory("Chutzpah")]
    public static class ChutzpahAliases
    {
        /// <summary>
        ///     Runs tests in the specified testFile (or in the working directory if null) with the specified settings (or default
        ///     settings if null).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="testPath">The path containing tests.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [CakeMethodAlias]
        public static void Chutzpah(this ICakeContext context, Path testPath = null,
            ChutzpahSettings settings = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var runner = new ChutzpahRunner(context.FileSystem, context.Environment,
                context.ProcessRunner, context.Globber);
            runner.Run(testPath, settings);
        }
    }
}
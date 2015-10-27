using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Utilities;

namespace Cake.Chutzpah
{
    /// <summary>
    /// </summary>
    public class ChutzpahRunner : Tool<ChutzpahSettings>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChutzpahRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="globber">The globber.</param>
        public ChutzpahRunner(IFileSystem fileSystem, ICakeEnvironment environment,
            IProcessRunner processRunner, IGlobber globber)
            : base(fileSystem, environment, processRunner, globber)
        {
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name => GetToolName();

        /// <summary>
        ///     Gets the tool executable names.
        /// </summary>
        /// <value>
        ///     The tool executable names.
        /// </value>
        public IEnumerable<string> ToolExecutableNames => GetToolExecutableNames();

        /// <summary>
        ///     Gets the name of the tool.
        /// </summary>
        /// <returns>
        ///     The name of the tool.
        /// </returns>
        protected override string GetToolName() => "Chutzpah";

        /// <summary>
        ///     Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>
        ///     The tool executable name.
        /// </returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "chutzpah.console.exe";
        }

        /// <summary>
        ///     Runs tests in the specified testFile (or in the working directory if null) with the specified settings (or default settings if null).
        /// </summary>
        /// <param name="testPath"></param>
        /// <param name="settings"></param>
        public void Run(Path testPath = null,ChutzpahSettings settings = null)
        {
            settings = settings ?? new ChutzpahSettings();

            var argBuilder = new ProcessArgumentBuilder();

            if (testPath != null)
            {
                argBuilder.Append(testPath.FullPath);
            }

            if (settings.NoLogo)
            {
                argBuilder.Append("/nologo");
            }

            if (settings.EnableCoverageCollection)
            {
                argBuilder.Append("/coverage");
            }

            if (settings.FailOnError)
            {
                argBuilder.Append("/failOnError");
            }

            if (settings.ForceTeamCityMode)
            {
                argBuilder.Append("/teamcity");
            }

            if (settings.CoverageHtmlOutputFile != null)
            {
                argBuilder.Append("/coveragehtml")
                    .AppendQuoted(settings.CoverageHtmlOutputFile.FullPath);
            }

            if (settings.LaunchInBrowser != ChutzpahBrowser.None)
            {
                argBuilder.Append("/openInBrowser");
                if (settings.LaunchInBrowser != ChutzpahBrowser.DefaultBrowser)
                {
                    argBuilder.Append(settings.LaunchInBrowser.ToString());
                }
            }

            if (settings.JUnitXmlResultsFile != null)
            {
                argBuilder.Append("/junit")
                    .AppendQuoted(settings.JUnitXmlResultsFile.FullPath);
            }

            if (settings.LcovResultsFile != null)
            {
                argBuilder.Append("/lcov")
                    .AppendQuoted(settings.LcovResultsFile.FullPath);
            }

            if (settings.VisualStudioTrxResultsFile != null)
            {
                argBuilder.Append("/trx")
                    .AppendQuoted(settings.VisualStudioTrxResultsFile.FullPath);
            }

            if (settings.NUnit2XmlResultsFile != null)
            {
                argBuilder.Append("/nunit2")
                    .AppendQuoted(settings.NUnit2XmlResultsFile.FullPath);
            }

            if (settings.MaxParallelism.HasValue)
            {
                argBuilder.Append("/parallelism {0}", settings.MaxParallelism);
            }

            if (!settings.OutputRunningTestCount)
            {
                argBuilder.Append("/silent");
            }

            if (settings.PrintDebugInfo)
            {
                argBuilder.Append("/debug");
            }

            if (settings.Trace)
            {
                argBuilder.Append("/trace");
            }

            if (settings.ShowFailureReport)
            {
                argBuilder.Append("/showFailureReport");
            }

            foreach (var path in settings.AdditionalTestPaths)
            {
                argBuilder.Append("/path").AppendQuoted(path.FullPath);
            }

            Run(settings, argBuilder);
        }
    }
}
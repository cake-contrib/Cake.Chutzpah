using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Chutzpah
{
    /// <summary>
    /// Contains settings used by <see cref="ChutzpahRunner"/>.
    /// </summary>
    public class ChutzpahSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to output running test count.
        /// Default is true
        /// </summary>
        /// <value>
        /// <c>true</c> if [output running test count]; otherwise, <c>false</c>.
        /// </value>
        public bool OutputRunningTestCount { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to force team city mode.
        /// (normally auto-detected) -- default is false
        /// </summary>
        /// <value>
        /// <c>true</c> if [force team city mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ForceTeamCityMode { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to return a non-zero exit code if any script errors or timeouts occurs
        /// </summary>
        /// <value>
        /// <c>true</c> if [fail on error]; otherwise, <c>false</c>.
        /// </value>
        public bool FailOnError { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to run without showing the copyright message.
        /// </summary>
        public bool NoLogo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to print debugging information and tracing to console.
        /// </summary>
        /// <value>
        /// <c>true</c> if [print debug information]; otherwise, <c>false</c>.
        /// </value>
        public bool PrintDebugInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to Log tracing information to chutzpah.log.
        /// </summary>
        /// <value>
        /// <c>true</c> if trace; otherwise, <c>false</c>.
        /// </value>
        public bool Trace { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to enable coverage collection.
        /// </summary>
        public bool EnableCoverageCollection { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to show a failure report after the test run.
        /// Useful if you have a large number of tests..
        /// </summary>
        public bool ShowFailureReport { get; set; } = false;

        /// <summary>
        /// Gets or sets an option to Launch the tests in a browser.
        /// If an option other than None is provided will try to launch in that browser.
        /// </summary>
        public ChutzpahBrowser LaunchInBrowser { get; set; } = ChutzpahBrowser.None;

        /// <summary>
        /// Gets or sets the Max degree of parallelism for Chutzpah.
        /// Defaults to number of CPUs + 1
        /// If you specify more than 1 the test output may be a bit jumbled
        /// </summary>
        public int? MaxParallelism { get; set; }

        /// <summary>
        /// Gets the list of test paths to run.
        /// Specify more than one to add multiple paths.
        /// If you give a folder, it will be scanned for testable files.
        /// </summary>
        public IList<Path> AdditionalTestPaths { get; } = new List<Path>();

        /// <summary>
        /// Gets or sets the file path for JUnit-style XML results output.
        /// If null (default), Chutzpah will not output JUnit xml results.
        /// </summary>
        public FilePath JUnitXmlResultsFile { get; set; }

        /// <summary>
        /// Gets or sets the file path for NUnit (v2.x) XML results output.
        /// If null (default), Chutzpah will not output NUnit xml results.
        /// </summary>
        public FilePath NUnit2XmlResultsFile { get; set; }

        /// <summary>
        /// Gets or sets the file path to output results as LCOV data for further processing.
        /// If null (default), Chutzpah will not output LCOV data results.
        /// </summary>
        public FilePath LcovResultsFile { get; set; }

        /// <summary>
        /// Gets or sets the file path for Visual Studio Trx results output.
        /// If null (default), Chutzpah will not output Visual Studio Trx results.
        /// </summary>
        public FilePath VisualStudioTrxResultsFile { get; set; }

        /// <summary>
        /// Gets or sets the file path default Chutzpah coverage HTML output.
        /// If null (default), Chutzpah will not output a coverage HTML file.
        /// </summary>
        public FilePath CoverageHtmlOutputFile { get; set; }

        // settingsFileEnvironment? Sets the environment properties for a chutzpah.json settings file.
        // Specify more than one to add multiple environments. (e.g.settingsFilePath; prop1=val1;prop2=val2).
    }
}
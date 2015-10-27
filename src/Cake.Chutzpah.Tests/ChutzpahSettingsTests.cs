using FluentAssertions;
using Xunit;

namespace Cake.Chutzpah.Tests
{
    public class ChutzpahSettingsTests
    {
        [Fact]
        public void OutputRunningTestCountByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.OutputRunningTestCount;
            result.Should().BeTrue();
        }

        [Fact]
        public void NoLogoIsFalseByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.NoLogo;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotForceTeamCityModeByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.ForceTeamCityMode;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotFailOnErrorByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.FailOnError;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotPrintDebugInfoByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.PrintDebugInfo;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotTraceByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.Trace;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotLaunchInBrowserByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.LaunchInBrowser;
            result.Should().Be(ChutzpahBrowser.None);
        }

        [Fact]
        public void MaxParallelismIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.MaxParallelism;
            result.HasValue.Should().BeFalse();
        }

        [Fact]
        public void AdditionalTestPathsIsEmptyByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.AdditionalTestPaths;
            result.Should().BeEmpty();
        }

        [Fact]
        public void DoNotEnableCoverageCollectionByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.EnableCoverageCollection;
            result.Should().BeFalse();
        }

        [Fact]
        public void DoNotShowFailureReportByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.ShowFailureReport;
            result.Should().BeFalse();
        }

        [Fact]
        public void JUnitXmlResultsFileIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.JUnitXmlResultsFile;
            result.Should().BeNull();
        }

        [Fact]
        public void LcovResultsFileIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.LcovResultsFile;
            result.Should().BeNull();
        }

        [Fact]
        public void VisualStudioTrxResultsFileIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.VisualStudioTrxResultsFile;
            result.Should().BeNull();
        }

        [Fact]
        public void NUnit2ResultsFileIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.NUnit2XmlResultsFile;
            result.Should().BeNull();
        }

        [Fact]
        public void CoverageHtmlOutputFileIsNullByDefault()
        {
            var sut = new ChutzpahSettings();
            var result = sut.CoverageHtmlOutputFile;
            result.Should().BeNull();
        }
    }
}

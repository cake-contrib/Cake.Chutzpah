using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Cake.Chutzpah.Tests
{
    public class ChutzpahRunnerTests
    {
        [Theory, CustomAutoData]
        public void ShouldImplementTool(ChutzpahRunner sut)
        {
            sut.Should().BeAssignableTo<Tool<ChutzpahSettings>>();
        }

        [Theory, CustomAutoData]
        public void ShouldBeNamedChutzpah(ChutzpahRunner sut)
        {
            sut.Name.Should().Be("Chutzpah");
        }

        [Theory, CustomAutoData]
        public void ShouldUseProperExecutable(ChutzpahRunner sut)
        {
            sut.ToolExecutableNames.Should().BeEquivalentTo("chutzpah.console.exe");
        }

        public class TheEmptyRunMethod
        {
            [Theory, CustomAutoData]
            public void ShouldSetWorkingDirectory([Frozen] ICakeEnvironment environment,
                [Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                environment.WorkingDirectory.Returns("/Working");
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);
                process.GetExitCode().Returns(0);

                sut.Run();

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(ps => ps.WorkingDirectory.FullPath == "/Working"));
            }

            [Theory, CustomAutoData]
            public void ShouldThrowIfProcessWasNotStarted([Frozen] ICakeEnvironment environment,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                environment.WorkingDirectory.Returns("/Working");
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);
                processRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>())
                    .Returns((IProcess)null);

                sut.Invoking(s => s.Run())
                    .ShouldThrow<CakeException>()
                    .WithMessage("Chutzpah: Process was not started.");
            }

            [Theory, CustomAutoData]
            public void ShouldThrowIfProcessHasANonZeroExitCode(
                [Frozen] IProcess process,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);
                process.GetExitCode().Returns(3);

                sut.Invoking(s => s.Run())
                    .ShouldThrow<CakeException>()
                    .WithMessage("Chutzpah: Process returned an error (exit code 3).");
            }

            [Theory, CustomAutoData]
            public void ShouldThrowIfChutzpahExecutableNotFound(
                [Frozen] ICakeEnvironment environment, [Frozen] IFileSystem fileSystem,
                ChutzpahRunner sut)
            {
                environment.WorkingDirectory.Returns("/Working");
                fileSystem.Exist(
                    Arg.Is<FilePath>(a => a.FullPath.Contains("chutzpah.console.exe")))
                    .Returns(false);

                sut.Invoking(x => x.Run())
                    .ShouldThrow<CakeException>()
                    .WithMessage("Chutzpah: Could not locate executable.");
            }

            [Theory, CustomAutoData]
            public void ShouldBuildEmptyCommand([Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                process.GetExitCode().Returns(0);
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

                sut.Run();

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(p => p.Arguments.Render() == string.Empty));
            }
        }

        public class TheRunMethodWithTestFile
        {
            [Theory, CustomAutoData]
            public void ShouldBuildCommandWithTestFilePath([Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                process.GetExitCode().Returns(0);
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

                sut.Run((FilePath)"mytests.js");

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(p => p.Arguments.Render() == "mytests.js"));
            }
        }

        public class TheRunMethodWithSettingsOnly
        {
            [Theory, CustomAutoData]
            public void ShouldUseEmptyCommandByDefault([Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                process.GetExitCode().Returns(0);
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

                var settings = new ChutzpahSettings();

                sut.Run(settings: settings);

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(p => p.Arguments.Render() == string.Empty));
            }

            [Theory, CustomAutoData]
            public void ShouldUseProvidedSettings([Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                process.GetExitCode().Returns(0);
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

                var settings = new ChutzpahSettings
                {
                    NoLogo = true,
                    EnableCoverageCollection = true,
                    FailOnError = true,
                    ForceTeamCityMode = true,
                    CoverageHtmlOutputFile = @"c:\temp\Code Coverage\coverage.html",
                    LaunchInBrowser = ChutzpahBrowser.DefaultBrowser,
                    JUnitXmlResultsFile = "junitResults.xml",
                    LcovResultsFile = "lcov.dat",
                    VisualStudioTrxResultsFile = "testResults.trx",
                    NUnit2XmlResultsFile = "nunit.xml",
                    MaxParallelism = 1,
                    OutputRunningTestCount = false,
                    PrintDebugInfo = true,
                    Trace = true,
                    ShowFailureReport = true,
                    AdditionalTestPaths = { (FilePath)"testfile.js", (DirectoryPath)"tests" }

                };

                sut.Run(settings: settings);

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(
                            p =>
                                p.Arguments.Render() ==
                                "/nologo /coverage /failOnError /teamcity /coveragehtml " +
                                "\"c:/temp/Code Coverage/coverage.html\" /openInBrowser " +
                                "/junit \"junitResults.xml\" /lcov \"lcov.dat\" /trx \"testResults.trx\" " +
                                "/nunit2 \"nunit.xml\" /parallelism 1 /silent /debug /trace " +
                                "/showFailureReport /path \"testfile.js\" /path \"tests\""));
            }

            [Theory, CustomAutoData]
            public void ShouldOpenInNonDefaultBrowser([Frozen] IProcess process,
                [Frozen] IProcessRunner processRunner,
                [Frozen] IFileSystem fileSystem, ChutzpahRunner sut)
            {
                process.GetExitCode().Returns(0);
                fileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

                var settings = new ChutzpahSettings
                {
                    LaunchInBrowser = ChutzpahBrowser.Chrome
                };

                sut.Run(settings: settings);

                processRunner.Received(1)
                    .Start(Arg.Any<FilePath>(),
                        Arg.Is<ProcessSettings>(
                            p => p.Arguments.Render() == "/openInBrowser Chrome"));
            }
        }
    }
}
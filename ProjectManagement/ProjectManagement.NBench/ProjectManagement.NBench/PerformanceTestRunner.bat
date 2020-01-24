set PATH=%PATH%;..\packages\NBench.Runner.1.0.4\lib\net452;
set perfTestReportFolder=PerformanceReport

rd /s /q %perfTestReportFolder%
mkdir %perfTestReportFolder%

NBench.Runner.exe .\bin\Debug\ProjectManagement.NBench.dll output-directory="%perfTestReportFolder%"




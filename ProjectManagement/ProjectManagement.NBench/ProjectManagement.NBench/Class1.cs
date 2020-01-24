using NBench;
using ProjectManagement.Controllers.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.NBench
{
    public class ProjectManagementPerfSpecs
    {
        private Counter _counter;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test to ensure the performance parameters of Get Projects API",
            NumberOfIterations = 10, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0.0d)]
        public void BenchmarkGetAllProjects()
        {
            var projectTest = new ProjectsControllerTests();
            projectTest.AddNewProject_ValidProject_ReturnsAllProjects();

            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure the performance parameters of Add Project API.",
            NumberOfIterations = 10, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0.0d)]
        public void BenchmarkAddNewProjects()
        {
            var ProjectsTest = new ProjectsControllerTests();
            ProjectsTest.AddNewProject_ValidProject_ReturnsAllProjects();

            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure the performance parameters of Get All Tasks API.",
            NumberOfIterations = 10, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0.0d)]
        public void BenchmarkGetAllTasks()
        {
            var TaskTests = new TasksControllerTests();
            TaskTests.GetTasks_All_ReturnAllTasks();

            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure the performance parameters of Add new task API.",
            NumberOfIterations = 10, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0.0d)]
        public void BenchmarkAddTasks()
        {
            var TAskTests = new TasksControllerTests();
            TAskTests.AddNewTask_ValidTask_ReturnsAllTasks();

            _counter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {

        }
    }
}

﻿using AnyStatus.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace AnyStatus.Monitors.Tests
{
    [TestClass]
    public class MonitoringTests
    {
        [TestMethod]
        public void HttpMonitor()
        {
            var request = new HttpStatus { Url = "http://www.microsoft.com" };

            new HttpStatusMonitor().Handle(request);

            Assert.AreSame(State.Ok, request.State);
        }

        [TestMethod]
        public void PingHandler()
        {
            var request = new Ping { Host = "localhost" };

            new PingMonitor().Handle(request);

            Assert.AreSame(State.Ok, request.State);
        }

        [TestMethod]
        public void TcpHandler()
        {
            var request = new TcpPort
            {
                Host = "www.microsoft.com",
                Port = 80
            };

            new TcpMonitor().Handle(request);

            Assert.AreSame(State.Ok, request.State);
        }

        [TestMethod]
        public void WindowsServiceHandler()
        {
            var request = new WindowsService
            {
                ServiceName = "Dhcp"
            };

            new WindowsServiceMonitor().Handle(request);

            Assert.AreSame(State.Ok, request.State);
        }

        [Ignore]
        [TestMethod]
        public void GitHubIssueHandler()
        {
            var request = new GitHubIssue
            {
                IssueNumber = 1,
                Repository = "AnyStatus",
                Owner = "AnyStatus"
            };

            new GitHubIssueMonitor().Handle(request);

            Assert.AreNotSame(State.None, request.State);
        }

        [TestMethod]
        public void CoverallsHandler()
        {
            var request = new CoverallsCoveredPercent
            {
                Url = "https://coveralls.io/github/xing/hardcover?branch=demo"
            };

            new CoverallsCoveredPercentMonitor().Handle(request);

            Assert.AreNotSame(State.None, request.State);
        }
        
        [TestMethod]
        public void UptimeRobotOverallStatus()
        {
            var logger = Substitute.For<ILogger>();

            var request = new UptimeRobot
            {
                ApiKey = "u131608-259ebe191e11db9a9e47aa51"
            };

            new UptimeRobotMonitor(logger).Handle(request);

            Assert.AreNotSame(State.None, request.State);
        }

        [TestMethod]
        public void UptimeRobotMonitorStatus()
        {
            var logger = Substitute.For<ILogger>();

            var request = new UptimeRobot
            {
                ApiKey = "u131608-259ebe191e11db9a9e47aa51",
                MonitorName = "Blog"
            };

            new UptimeRobotMonitor(logger).Handle(request);

            Assert.AreNotSame(State.None, request.State);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NSubstitute;
using NUnit.Framework;
using showTracker.BusinessLayer.Interfaces;
using showTracker.BusinessLayer.Services;
using showTracker.BusinessLayer.Wrappers;
using showTracker.Model.API.Dto;
using Assert = NUnit.Framework.Assert;

namespace showTracker.BusinessLayer.Tests.Api
{
    [TestFixture]
    public class ApiClientTests
    {
        private readonly ShowDto _mockShowDto = new ShowDto
        {
            Name = "Sth",
            Runtime = 40
        };

        private readonly IEnumerable<EpisodeDto> _mockEpisodeDto = new EpisodeDto[]
        {
            new EpisodeDto
            {
                Name = "Ep1",
                Number = 1
            },
            new EpisodeDto
            {
                Name = "Ep2",
                Number = 2
            }
        };

        private IJsonSerializeService _jsonSerializeService;

        [SetUp]
        public void Init()
        {
            _jsonSerializeService = new JsonSerializeService();
        }

        [Test]
        public void GetShows_ValidId_ReturnsShow()
        {
            //Arrange
            var showService = Substitute.For<IShowService>();
            showService.GetShow(Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockShowDto));
            var apiClient = new ApiClientService(_jsonSerializeService, showService);

            //Act
            var show = apiClient.GetShow(1);
            show.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockShowDto), _jsonSerializeService.SerializeObject(show.Result));
        }

        [Test]
        public void RealGetShows_ValidId_ReturnsShow()
        {
            //Arrange
            var apiClient = new ApiClientService(_jsonSerializeService, new ShowService(new HttpClientWrapper()));

            //Act
            var result = apiClient.GetShow(1);
            result.Wait();

            //Arrange
            Assert.AreNotEqual(null, result.Result);
        }

        // This test is only for DEBUG purpouse
        // Shows how to test code withoutrunning real App
        // Use this approach to test methods like this
        // Use Debug to attach to specific line of code and see if your Dto is valid
        // If not sure contact me (@TB) by Slack/FB
        [Test]
        public void TestRealShow()
        {
            var show = new ShowService(new HttpClientWrapper()).GetShow(1);
            show.Wait();
            var showObj = _jsonSerializeService.DeserializeObject<ShowDto>(show.Result);

            Assert.Pass();        
        }


        [Test]
        public void GetEpisodes_ValidId_ReturnEpisodes()
        {
            //Arrange
            var showService = Substitute.For<IShowService>();
            showService.GetShow(Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockShowDto));
            var episodeService = Substitute.For<IEpisodeService>();
            episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<bool>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto));
            var apiClient = new ApiClientService(_jsonSerializeService, showService, episodeService);

            //Act
            var episode = apiClient.GetEpisodes(1, true);
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockEpisodeDto), _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodes_ValidId_ReturnEpisodes()
        {
            //Arrange
            var apiClient = new ApiClientService(_jsonSerializeService, new ShowService(new HttpClientWrapper()), new EpisodeService(new HttpClientWrapper()));

            //Act
            var result = apiClient.GetEpisodes(1, true);
            result.Wait();

            //Arrange
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void GetEpisodesByDate_ValidId_ReturnEpisodes()
        {
            //Arrange
            var showService = Substitute.For<IShowService>();
            showService.GetShow(Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockShowDto));
            var episodeService = Substitute.For<IEpisodeService>();
            episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<System.DateTime>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto));
            var apiClient = new ApiClientService(_jsonSerializeService, showService, episodeService);

            //Act
            var episode = apiClient.GetEpisodes(1, new DateTime());
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockEpisodeDto), _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodesByDate_ValidId_ReturnEpisodes()
        {
            //Arrange
            var apiClient = new ApiClientService(_jsonSerializeService, new ShowService(new HttpClientWrapper()), new EpisodeService(new HttpClientWrapper()));

            //Act
            var result = apiClient.GetEpisodes(1, new DateTime(2013, 7, 1));
            result.Wait();

            //Arrange
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void GetEpisodesByNumber_ValidId_ReturnEpisodes()
        {
            //Arrange
            var showService = Substitute.For<IShowService>();
            showService.GetShow(Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockShowDto));
            var episodeService = Substitute.For<IEpisodeService>();
            episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto));
            var apiClient = new ApiClientService(_jsonSerializeService, showService, episodeService);

            //Act
            var episode = apiClient.GetEpisodes(1, 1, 1);
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockEpisodeDto), _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodesByNumber_ValidId_ReturnEpisodes()
        {
            //Arrange
            var apiClient = new ApiClientService(_jsonSerializeService, new ShowService(new HttpClientWrapper()), new EpisodeService(new HttpClientWrapper()));

            //Act
            var result = apiClient.GetEpisodes(1, 1, 1);
            result.Wait();

            //Arrange
            Assert.AreNotEqual(null, result.Result);
        }
    }
}

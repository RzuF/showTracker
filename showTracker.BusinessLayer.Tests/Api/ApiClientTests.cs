using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

        private readonly IEnumerable<ShowDto> _mockShowDtos = new[]
        {
            new ShowDto
            {
                Name = "Dth",
                Runtime = 40
            },
            new ShowDto
            {
                Name = "Fth",
                Runtime = 40
            }
        };

        private readonly IEnumerable<EpisodeDto> _mockEpisodeDto = new[]
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

        private readonly IEnumerable<PeopleDto> _mockPeopleDtos = new[]
        {
            new PeopleDto
            {
                Name = "Lara Smith",
                Gender = "Female"
            },
            new PeopleDto
            {
                Name = "Nico Pico",
                Gender = "Male"
            }
        };

        private readonly IEnumerable<SeasonDto> _mockSeasonDtos = new[]
        {
            new SeasonDto
            {
                Number = 1
            },
            new SeasonDto
            {
                Number = 2
            }
        };

        private readonly IEnumerable<CrewDto> _mockCrewDtos = new[]
        {
            new CrewDto
            {
                Type = "Director"
            },
            new CrewDto
            {
                Type = "Creator"
            }
        };

        private readonly IEnumerable<CastDto> _mockCastDtos = new[]
        {
            new CastDto
            {
                Person = new PeopleDto {Id = 1}
            },
            new CastDto
            {
                Person = new PeopleDto {Id = 2}
            }
        };

        private readonly IEnumerable<AkaDto> _mockAkaDtos = new[]
        {
            new AkaDto
            {
                Name = "Sth"
            },
            new AkaDto
            {
                Name = "Sth in other language"
            }
        };

        private IJsonSerializeService _jsonSerializeService;
        private IShowService _showService;
        private IEpisodeService _episodeService;
        private IApiClientService _apiClientMock;
        private IApiClientService _apiClientTrue;
        private ISearchService _searchService;
        private IShowExtendedService _showExtendedService;
        private readonly HttpClientWrapper _httpClientWrapper = new HttpClientWrapper();

        [SetUp]
        public void Init()
        {
            _jsonSerializeService = new JsonSerializeService();
            _showService = Substitute.For<IShowService>();
            _episodeService = Substitute.For<IEpisodeService>();
            _searchService = Substitute.For<ISearchService>();
            _showExtendedService = Substitute.For<IShowExtendedService>();
            _apiClientMock = new ApiClientService(_jsonSerializeService, _showService, _episodeService, _searchService, _showExtendedService);
            _apiClientTrue = new ApiClientService(_jsonSerializeService,
                new ShowService(_httpClientWrapper),
                new EpisodeService(_httpClientWrapper),
                new SearchService(_httpClientWrapper),
                new ShowExtendedService(_httpClientWrapper));
        }

        [Test]
        public void GetShows_ValidId_ReturnsShow()
        {
            //Arrange
            _showService.GetShow(Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockShowDto));

            //Act
            var show = _apiClientMock.GetShow(1);
            show.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockShowDto), _jsonSerializeService.SerializeObject(show.Result));
        }

        [Test]
        public void RealGetShows_ValidId_ReturnsShow()
        {
            //Arrange
            

            //Act
            var result = _apiClientTrue.GetShow(1);
            result.Wait();

            //Assert
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

        //EpisodeService Tests

        [Test]
        public void GetEpisodes_ValidId_ReturnEpisodes()
        {
            //Arrange
            _episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<bool>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto));

            //Act
            var episode = _apiClientMock.GetEpisodes(1, true);
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockEpisodeDto), _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodes_ValidId_ReturnEpisodes()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetEpisodes(1, true);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void GetEpisodesByDate_ValidId_ReturnEpisodes()
        {
            //Arrange
            _episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<DateTime>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto));

            //Act
            var episode = _apiClientMock.GetEpisodes(1, new DateTime());
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockEpisodeDto), _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodesByDate_ValidId_ReturnEpisodes()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetEpisodes(1, new DateTime(2013, 7, 1));
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void GetEpisodesByNumber_ValidId_ReturnEpisodes()
        {
            //Arrange
            _episodeService.GetEpisodes(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(_jsonSerializeService.SerializeObject(_mockEpisodeDto.First()));

            //Act
            var episode = _apiClientMock.GetEpisodes(1, 1, 1);
            episode.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(new [] {_mockEpisodeDto.First()}) , _jsonSerializeService.SerializeObject(episode.Result));
        }

        [Test]
        public void RealGetEpisodesByNumber_ValidId_ReturnEpisodes()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetEpisodes(1, 1, 1);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        //SearchService Tests

        [Test]
        public void SearchSingleShow_ValidQuery_ReturnShows()
        {
            //Arrange
            _searchService.SearchShows(Arg.Any<string>(), true)
                .Returns(_jsonSerializeService.SerializeObject(_mockShowDto));

            //Act
            var show = _apiClientMock.SearchShow("sample text");
            show.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockShowDto), _jsonSerializeService.SerializeObject(show.Result));
        }

        [Test]
        public void RealSearchSingleShow_ValidQuery_ReturnShows()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.SearchShow("girls");
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void SearchShows_ValidQuery_ReturnShows()
        {
            //Arrange
            _searchService.SearchShows(Arg.Any<string>(), false)
                .Returns(_jsonSerializeService.SerializeObject(_mockShowDtos));

            //Act
            var shows = _apiClientMock.SearchShows("sample text", false);
            shows.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockShowDtos), _jsonSerializeService.SerializeObject(shows.Result));
        }

        [Test]
        public void RealSearchShows_ValidQuery_ReturnShows()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.SearchShows("girls", false);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void SearchPeople_ValidQuery_ReturnPeople()
        {
            //Arrange
            _searchService.SearchPeople(Arg.Any<string>()).Returns(_jsonSerializeService.SerializeObject(_mockPeopleDtos));

            //Act
            var shows = _apiClientMock.SearchPeople("sample text");
            shows.Wait();

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockPeopleDtos), _jsonSerializeService.SerializeObject(shows.Result));
        }

        [Test]
        public void RealSearchPeople_ValidQuery_ReturnPeople()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.SearchPeople("lauren");
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        //ShowExtendedService

        [Test]
        public void ShowExtendedSeasons_ValidId_ReturnSeasons()
        {
            //Arrange
            _showExtendedService.GetSeasons(Arg.Any<int>())
                .Returns(_jsonSerializeService.SerializeObject(_mockSeasonDtos));

            //Act
            var seasons = _apiClientMock.GetSeasons(1);

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockSeasonDtos), _jsonSerializeService.SerializeObject(seasons.Result));
        }

        [Test]
        public void RealShowExtendedSeasons_ValidId_ReturnSeasons()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetSeasons(1);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void ShowExtendedCast_ValidId_ReturnCast()
        {
            //Arrange
            _showExtendedService.GetCast(Arg.Any<int>())
                .Returns(_jsonSerializeService.SerializeObject(_mockCastDtos));

            //Act
            var cast = _apiClientMock.GetCast(1);

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockCastDtos), _jsonSerializeService.SerializeObject(cast.Result));
        }

        [Test]
        public void RealShowExtendedCast_ValidId_ReturnCast()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetCast(1);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void ShowExtendedCrew_ValidId_ReturnCrew()
        {
            //Arrange
            _showExtendedService.GetCrew(Arg.Any<int>())
                .Returns(_jsonSerializeService.SerializeObject(_mockCrewDtos));

            //Act
            var crew = _apiClientMock.GetCrew(1);

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockCrewDtos), _jsonSerializeService.SerializeObject(crew.Result));
        }

        [Test]
        public void RealShowExtendedCrew_ValidId_ReturnCrew()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetCrew(1);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }

        [Test]
        public void ShowExtendedAkas_ValidId_ReturnAkas()
        {
            //Arrange
            _showExtendedService.GetAkas(Arg.Any<int>())
                .Returns(_jsonSerializeService.SerializeObject(_mockAkaDtos));

            //Act
            var akas = _apiClientMock.GetAkas(1);

            //Assert
            Assert.AreEqual(_jsonSerializeService.SerializeObject(_mockAkaDtos), _jsonSerializeService.SerializeObject(akas.Result));
        }

        [Test]
        public void RealShowExtendedAkas_ValidId_ReturnAkas()
        {
            //Arrange

            //Act
            var result = _apiClientTrue.GetAkas(1);
            result.Wait();

            //Assert
            Assert.AreNotEqual(null, result.Result);
        }
    }
}

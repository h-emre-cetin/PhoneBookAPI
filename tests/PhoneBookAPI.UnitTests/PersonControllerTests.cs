using Moq;
using NUnit.Framework;
using YourNamespace.Controllers;
using YourNamespace.Services;
using YourNamespace.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookAPI.UnitTests;

public class PersonControllerTests
{
   private Mock<IPersonService> _mockPersonService;
    private Mock<ICacheService> _mockCacheService;
    private PersonController _controller;

    [SetUp]
    public void Setup()
    {
        _mockPersonService = new Mock<IPersonService>();
        _mockCacheService = new Mock<ICacheService>();
        _controller = new PersonController(_mockPersonService.Object, _mockCacheService.Object);
    }
}
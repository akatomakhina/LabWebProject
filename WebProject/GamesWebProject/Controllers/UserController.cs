using GamesProject.Logic.Common.Services;
using NLog;
using System;
using System.ServiceModel;

namespace GamesWebProject.Controllers
{
    [ServiceContract]
    public class UserController
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
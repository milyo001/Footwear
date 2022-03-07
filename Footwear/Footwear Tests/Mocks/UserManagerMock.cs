using Footwear.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

public class UserManagerMock : UserManager<User>
{
    //, ILookupNormalizer, IdentityErrorDescriber, IServiceProvider, ILogger<UserManager<TUser>>)
    public UserManagerMock(IUserStore<User> userStore, IOptions<IdentityOptions> identityOptions, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer lookupNormalizer, IdentityErrorDescriber identityErrorDescriber, IServiceProvider serviceProvider,
        ILogger<UserManager<User>> logger)
        : base(userStore, identityOptions, passwordHasher, userValidators, passwordValidators, lookupNormalizer,
            identityErrorDescriber, serviceProvider, logger)
    {
    }
}
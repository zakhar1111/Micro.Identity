﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class AspNetIdentityDbContext 
    : IdentityDbContext
{
    public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> opt)
        : base(opt) { }
}

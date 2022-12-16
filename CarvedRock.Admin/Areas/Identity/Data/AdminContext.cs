﻿using CarvedRock.Admin.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Data;	// should this namespace be CarvedRock.Admin.Areas.Identity.Data?

public class AdminContext : IdentityDbContext<AdminUser>
{
	private readonly string _dbPath;

	public AdminContext(IConfiguration configuration)
	{
		var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		_dbPath = Path.Join(path, configuration.GetConnectionString("UserDbFilename"));
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options) 
		=> options.UseSqlite($"Data Source={_dbPath}");
}

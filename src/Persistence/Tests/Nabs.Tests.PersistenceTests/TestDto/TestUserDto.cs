﻿using AutoMapper;
using AutoMapper.EF6;

namespace Nabs.Tests.PersistenceTests.TestDto;

public class TestUserDto : IDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}
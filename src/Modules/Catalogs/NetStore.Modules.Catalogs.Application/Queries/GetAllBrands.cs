﻿using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries;

public sealed record GetAllBrands : IQuery<IEnumerable<BrandDto>>;
﻿using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record PasswordRecovered(Guid UserId) : IEvent;
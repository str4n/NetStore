﻿using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record UserSignedUp(Guid UserId, string Email, string Username) : IEvent;
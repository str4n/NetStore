using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.EF.Configurations;

internal sealed class ActivationTokenConfiguration : IEntityTypeConfiguration<ActivationToken>
{
    public void Configure(EntityTypeBuilder<ActivationToken> builder)
    {
        builder.HasKey(x => x.Secret);
    }
}
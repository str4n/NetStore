using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.EF.Configurations;

internal sealed class RecoveryTokenConfiguration : IEntityTypeConfiguration<RecoveryToken>
{
    public void Configure(EntityTypeBuilder<RecoveryToken> builder)
    {
        builder.HasKey(x => x.Token);
    }
}
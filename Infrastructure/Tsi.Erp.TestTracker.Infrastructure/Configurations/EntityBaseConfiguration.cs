namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;

public class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T>
    where T : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        private EntityTypeBuilder<T> _builder;

        public virtual void KeyBuilder(BaseKeyBuilder<T> builder)
        {
            _builder.Property<int>("ID");

            builder.HasKey("ID");
        }

        public abstract void ConfigureProperty(BasePropertyBuilder<T> builder);
        public virtual void ConfigureIndex(BaseIndexBuilder<T> builder) { }
        public virtual void ConfigureRelationship(BaseRelationshipBuilder<T> builder) { }
        public virtual void SeedData(BaseSeeder<T> builder) { }

        public virtual void BuildAuditing()
        {
            _builder.Property<DateTime>("CreatedOn")
                .HasDefaultValueSql("GETUTCDATE()");

            _builder.Property<string>("CreatedBy")
                .HasDefaultValueSql("SUSER_NAME()")
                .HasMaxLength(50)
                .IsRequired();

            _builder.Property<DateTime?>("ModifiedOn");
            _builder.Property<string>("ModifiedBy")
                .HasMaxLength(50);

            _builder.Property<byte[]>("LastChanged")
                .IsRowVersion();
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            _builder = builder;

            ConfigureProperty(new BasePropertyBuilder<T>(builder));

            KeyBuilder(new BaseKeyBuilder<T>(builder));

            ConfigureIndex(new BaseIndexBuilder<T>(builder));
            ConfigureRelationship(new BaseRelationshipBuilder<T>(builder));

            SeedData(new BaseSeeder<T>(builder));

            BuildAuditing();
        }

        public class BasePropertyBuilder<TEntity> where TEntity : class
        {
            private readonly EntityTypeBuilder<TEntity> builder;


            public BasePropertyBuilder(EntityTypeBuilder<TEntity> builder)
            {
                this.builder = builder;
            }

            public PropertyBuilder<TProperty> Property<TProperty>([NotNullAttribute] Expression<Func<TEntity, TProperty>> propertyExpression)
            {
                return builder.Property(propertyExpression);
            }
            public PropertyBuilder<TProperty> Property<TProperty>([NotNullAttribute] string propertyName)
            {
                return builder.Property<TProperty>(propertyName);
            }
        }

        public class BaseRelationshipBuilder<TEntity> where TEntity : class
        {
            private readonly EntityTypeBuilder<TEntity> builder;

            public BaseRelationshipBuilder(EntityTypeBuilder<TEntity> builder)
            {
                this.builder = builder;
            }

            public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(string navigationName) where TRelatedEntity : class
            {
                return builder.HasOne<TRelatedEntity>(navigationName);
            }

            public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> navigationExpression = null) where TRelatedEntity : class
            {
                return builder.HasOne<TRelatedEntity>(navigationExpression);
            }

            public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression = null) where TRelatedEntity : class
            {
                return builder.HasMany(navigationExpression);
            }

            public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(string navigationName) where TRelatedEntity : class
            {
                return builder.HasMany<TRelatedEntity>(navigationName);
            }
        }

        public class BaseSeeder<TEntity> where TEntity : class
        {
            private readonly EntityTypeBuilder<TEntity> builder;

            public BaseSeeder(EntityTypeBuilder<TEntity> builder)
            {
                this.builder = builder;
            }

            public void HasData([NotNullAttribute] IEnumerable<object> data)
            {
                builder.HasData(data);
            }

            public void HasData([NotNullAttribute] params object[] data)
            {
                builder.HasData(data);
            }

            public void HasData([NotNullAttribute] IEnumerable<TEntity> data)
            {
                builder.HasData(data);
            }

            public void HasData([NotNullAttribute] params TEntity[] data)
            {
                builder.HasData(data);
            }
        }

        public class BaseIndexBuilder<TEntity> where TEntity : class
        {
            private readonly EntityTypeBuilder<TEntity> builder;

            public BaseIndexBuilder(EntityTypeBuilder<TEntity> builder)
            {
                this.builder = builder;
            }

            public IndexBuilder HasIndex([NotNullAttribute] params string[] propertyNames)
            {
                return builder.HasIndex(propertyNames);
            }

            public IndexBuilder<TEntity> HasIndex([NotNullAttribute] Expression<Func<TEntity, object>> indexExpression)
            {
                return builder.HasIndex(indexExpression);
            }
        }

        public class BaseKeyBuilder<TEntity> where TEntity : class
        {
            private readonly EntityTypeBuilder<TEntity> builder;

            public BaseKeyBuilder(EntityTypeBuilder<TEntity> builder)
            {
                this.builder = builder;
            }

            public KeyBuilder HasKey([NotNullAttribute] params string[] propertyNames)
            {
                return builder.HasKey(propertyNames);
            }
        }
    }
}

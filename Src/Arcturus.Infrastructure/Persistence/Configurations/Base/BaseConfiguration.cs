using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Arcturus.Infrastructure.Persistence.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        private EntityTypeBuilder<T> m_EntityTypeBuilder;

        protected virtual string Schema
        {
            get
            {
                return "dbo";
            }
        }

        protected virtual string TableName
        {
            get
            {
                //return $"tbl_{typeof(T).Name}";
                 return $"{Pluralize(typeof(T).Name)}";
            }
        }

        protected virtual void KeyBuilder(BaseKeyBuilder<T> builder)
        {
            m_EntityTypeBuilder.Property<int>("ID").ValueGeneratedOnAdd();
            //m_EntityTypeBuilder.Property<Guid>("ID").ValueGeneratedOnAdd();

            builder.HasKey("ID");
        }

        protected virtual void ConfigureProperty(BasePropertyBuilder<T> builder) { }
        protected virtual void ConfigureIndex(BaseIndexBuilder<T> builder) { }
        protected virtual void ConfigureRelationship(BaseRelationshipBuilder<T> builder) { }
        protected virtual void SeedData(BaseSeeder<T> builder) { }

        public string GetObjectName()
        {
            return $"{Schema}.{TableName}";
        }

        protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder) { }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            m_EntityTypeBuilder = builder;
            builder.ToTable(TableName, Schema);

            SetDecimalPrecisions(builder);
            ConfigureProperty(new BasePropertyBuilder<T>(builder));
            KeyBuilder(new BaseKeyBuilder<T>(builder));
            ConfigureIndex(new BaseIndexBuilder<T>(builder));
            ConfigureRelationship(new BaseRelationshipBuilder<T>(builder));
            ConfigureEntity(builder);

            SeedData(new BaseSeeder<T>(builder));
        }

        protected virtual string Pluralize(string input)
        {
            string _retVal = input ?? "";

            if (_retVal.Trim() == "") return "";
            if (_retVal.EndsWith("Data")) return _retVal;
            if (_retVal.EndsWith("ch")) return _retVal + "es";
            if (_retVal.EndsWith("sis")) return _retVal.Substring(0, _retVal.Length - 3) + "ses";
            if (_retVal.EndsWith("s")) return _retVal + "es";
            if (_retVal.EndsWith("ay")) return _retVal + "s";
            if (_retVal.EndsWith("y")) return _retVal.Substring(0, _retVal.Length - 1) + "ies";

            return _retVal + "s";
        }

        protected virtual void SetDecimalPrecisions(EntityTypeBuilder<T> builder, int precision = 20, int scale = 6)
        {
            var _properties = typeof(T).GetProperties()
               .Where(p => p.PropertyType == typeof(decimal)
                        || p.PropertyType == typeof(decimal?))
               .Select(a => a.Name)
               .ToList();

            foreach (var _prop in _properties)
            {
                builder.Property(_prop).HasColumnType($"DECIMAL({precision},{scale})");
            }
        }

        protected abstract class BaseBuilder<TEntity> where TEntity : class
        {
            protected readonly EntityTypeBuilder<TEntity> m_Builder;

            public BaseBuilder(EntityTypeBuilder<TEntity> builder)
            {
                m_Builder = builder;
            }
        }

        protected class BasePropertyBuilder<TEntity> : BaseBuilder<TEntity>
            where TEntity : class
        {
            public BasePropertyBuilder(EntityTypeBuilder<TEntity> builder) : base(builder) { }

            public PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
            {
                return m_Builder.Property(propertyExpression);
            }
            public PropertyBuilder<TProperty> Property<TProperty>(string propertyName)
            {
                return m_Builder.Property<TProperty>(propertyName);
            }
        }

        protected class BaseRelationshipBuilder<TEntity> : BaseBuilder<TEntity>
            where TEntity : class
        {
            public BaseRelationshipBuilder(EntityTypeBuilder<TEntity> builder) : base(builder) { }

            public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(string navigationName) where TRelatedEntity : class
            {
                return m_Builder.HasOne<TRelatedEntity>(navigationName);
            }

            public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> navigationExpression = null) where TRelatedEntity : class
            {
                return m_Builder.HasOne<TRelatedEntity>(navigationExpression);
            }

            public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression = null) where TRelatedEntity : class
            {
                return m_Builder.HasMany(navigationExpression);
            }

            public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(string navigationName) where TRelatedEntity : class
            {
                return m_Builder.HasMany<TRelatedEntity>(navigationName);
            }
        }

        protected class BaseSeeder<TEntity> : BaseBuilder<TEntity>
            where TEntity : class
        {
            public BaseSeeder(EntityTypeBuilder<TEntity> builder) : base(builder) { }

            public void HasData(IEnumerable<object> data)
            {
                m_Builder.HasData(data);
            }

            public void HasData(params object[] data)
            {
                m_Builder.HasData(data);
            }

            public void HasData(IEnumerable<TEntity> data)
            {
                m_Builder.HasData(data);
            }

            public void HasData(params TEntity[] data)
            {
                m_Builder.HasData(data);
            }
        }

        protected class BaseIndexBuilder<TEntity> : BaseBuilder<TEntity>
            where TEntity : class
        {
            public BaseIndexBuilder(EntityTypeBuilder<TEntity> builder) : base(builder) { }

            public IndexBuilder HasIndex(params string[] propertyNames)
            {
                return m_Builder.HasIndex(propertyNames);
            }

            public IndexBuilder<TEntity> HasIndex(Expression<Func<TEntity, object>> indexExpression)
            {
                return m_Builder.HasIndex(indexExpression);
            }
        }

        protected class BaseKeyBuilder<TEntity> : BaseBuilder<TEntity>
            where TEntity : class
        {
            public BaseKeyBuilder(EntityTypeBuilder<TEntity> builder) : base(builder) { }

            public KeyBuilder HasKey(params string[] propertyNames)
            {
                return m_Builder.HasKey(propertyNames);
            }

            public KeyBuilder HasKey(Expression<Func<TEntity, object>> keyExpression)
            {
                return m_Builder.HasKey(keyExpression);
            }
        }
    }
}

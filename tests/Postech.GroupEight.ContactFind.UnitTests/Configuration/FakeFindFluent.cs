﻿using MongoDB.Driver;

namespace Postech.GroupEight.ContactFind.UnitTests.Configuration
{
    public class FakeFindFluent<TEntity> : IFindFluent<TEntity, TEntity>
    {
        private readonly IEnumerable<TEntity> _items;

        public FakeFindFluent(IEnumerable<TEntity> items)
        {
            _items = items ?? Enumerable.Empty<TEntity>();
        }

        public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_items.FirstOrDefault());
        }

        public IFindFluent<TEntity, TResult> As<TResult>(
            MongoDB.Bson.Serialization.IBsonSerializer<TResult> resultSerializer = null)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult((long)_items.Count());
        }

        public FilterDefinition<TEntity> Filter
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IFindFluent<TEntity, TEntity> Limit(int? limit)
        {
            return this;
        }

        public FindOptions<TEntity, TEntity> Options
        {
            get { throw new NotImplementedException(); }
        }

        public IFindFluent<TEntity, TNewProjection> Project<TNewProjection>(
            ProjectionDefinition<TEntity, TNewProjection> projection)
        {
            throw new NotImplementedException();
        }

        public IFindFluent<TEntity, TEntity> Skip(int? skip)
        {
            return this;
        }

        public IFindFluent<TEntity, TEntity> Sort(SortDefinition<TEntity> sort)
        {
            return this;
        }

        public Task<IAsyncCursor<TEntity>> ToCursorAsync(CancellationToken cancellationToken)
        {
            IAsyncCursor<TEntity> cursor = new FakeAsyncCursor<TEntity>(_items);
            var task = Task.FromResult(cursor);

            return task;
        }

        public long Count(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public long CountDocuments(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountDocumentsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TEntity> ToCursor(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeAsyncCursor<TEntity> : IAsyncCursor<TEntity>
    {
        private readonly IEnumerable<TEntity> _items;
        private IEnumerator<TEntity> _enumerator;

        public FakeAsyncCursor(IEnumerable<TEntity> items)
        {
            _items = items ?? Enumerable.Empty<TEntity>();
            _enumerator = _items.GetEnumerator();
        }

        public IEnumerable<TEntity> Current => new List<TEntity> { _enumerator.Current };

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            return _enumerator.MoveNext();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MoveNext(cancellationToken));
        }

        public void Reset()
        {
            _enumerator = _items.GetEnumerator();
        }
    }


    public static class MongoCollectionExtensions
    {
        public static IFindFluent<TEntity, TEntity> Find<TEntity>(
            this IMongoCollection<TEntity> collection,
            FilterDefinition<TEntity> filter)
        {
            return collection.Find(filter, null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Meteors.AspNetCore.Service.BoundedContext.Store
{
    public interface _IStore { }
    public interface _IQuery { }
    public interface _IFilter { }
    public interface IMrStore<TQuery, TFilter> : _IStore where TQuery : _IQuery where TFilter : _IFilter {}

    public abstract class MrStore<TQuery, TFilter> : IMrStore<TQuery, TFilter> where TQuery : _IQuery ,new() where TFilter : _IFilter, new()
    {
        public readonly TQuery Query;
        public readonly TFilter Filter;
        protected MrStore()
        {
            Query = new TQuery();
            Filter = new TFilter();
        }
    }

    public class _FakeStore : _IStore {}

    /// <summary>
    /// Enable dynamic use for any Store
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    [MrStore]
    public class _ContainerStore<TQuery, TFilter> : MrStore<TQuery, TFilter> where TQuery : _IQuery, new() where TFilter : _IFilter, new()
    {
        //should be empty
    }
}

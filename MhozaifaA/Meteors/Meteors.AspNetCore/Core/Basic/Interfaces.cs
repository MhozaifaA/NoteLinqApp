﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Core
{
    /// <summary>
    /// Enable struct key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKey<T> 
    {
        /// <summary>
        /// Key property
        /// </summary>
        public T Key { get; set; }
    }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public interface IKey : IKey<string> { }


    /// <summary>
    /// Enable name field
    /// </summary>
    public interface INominal
    {
        /// <summary>
        /// Name property allow to handle filter,validation
        /// </summary>
        public string Name { get; set; }
    }

}

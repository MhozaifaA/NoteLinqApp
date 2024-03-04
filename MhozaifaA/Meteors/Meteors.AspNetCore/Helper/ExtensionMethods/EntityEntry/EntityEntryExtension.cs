using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meteors.AspNetCore.Helper.ExtensionMethods.EntityEntry
{
    public static class EntityEntryExtension
    {
        public static TEntity ToEntity<TEntity> (this Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
            => entry.Entity.CastTo<TEntity>();

    }
}

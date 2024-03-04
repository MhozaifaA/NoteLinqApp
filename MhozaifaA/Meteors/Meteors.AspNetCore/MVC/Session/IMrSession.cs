using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    public interface IMrSession
    {
        TObject? Get<TObject>(string key);
        TObject? Get<TObject>();
        void Set<TObject>(string key, TObject value);
        void Set<TObject>(TObject value);
    }
}

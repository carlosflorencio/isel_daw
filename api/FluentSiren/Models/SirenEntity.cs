using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSiren.Models
{
    public class SirenEntity
    {

        internal SirenEntity()
        {

        }

        public IReadOnlyList<string> Class { get; internal set; }
        public IReadOnlyDictionary<string, object> Properties { get; internal set; }
        public IReadOnlyList<SubEntity> Entities { get; internal set; }
        public IReadOnlyList<Link> Links { get; internal set; }
        public IReadOnlyList<Action> Actions { get; internal set; }
        public string Title { get; internal set; }
    }
}

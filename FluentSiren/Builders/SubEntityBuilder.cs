using FluentSiren.Models;

namespace FluentSiren.Builders
{
    public abstract class SubEntityBuilder : SirenEntityBuilder
    {
        public new abstract SubEntity Build();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentSiren.Models;

namespace FluentSiren.Builders
{
    public class EmbeddedRepresentationBuilder : SubEntityBuilder
    {
        private List<string> _rel;

        public EmbeddedRepresentationBuilder WithRel(string rel)
        {
            if (_rel == null)
                _rel = new List<string>();

            _rel.Add(rel);
            return this;
        }

        public override SubEntity Build()
        {
            if (_rel == null || !_rel.Any())
                throw new ArgumentException("Rel is required.");

            var subEntity = new SubEntity()
            {
                Class = _class?.ToArray(),
                Rel = _rel?.ToArray(),
                Properties = _properties?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Entities = _subEntityBuilders?.Select(x => x.Build()).ToArray(),
                Links = _linkBuilders?.Select(x => x.Build()).ToArray(),
                Actions = _actionBuilders?.Select(x => x.Build()).ToArray(),
                Title = _title
            };

            if (subEntity.Actions != null && new HashSet<string>(subEntity.Actions.Select(x => x.Name)).Count != subEntity.Actions.Count)
                throw new ArgumentException("Action names MUST be unique within the set of actions for an entity.");

            return subEntity;
        }
    }
}
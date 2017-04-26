using System;
using System.Collections.Generic;
using System.Linq;
using FluentSiren.Models;

namespace FluentSiren.Builders
{
    public class SirenEntityBuilder
    {
        protected List<ActionBuilder> _actionBuilders;
        protected List<string> _class;
        protected List<LinkBuilder> _linkBuilders;
        protected Dictionary<string, object> _properties;
        protected List<SubEntityBuilder> _subEntityBuilders;
        protected string _title;

        public SirenEntityBuilder WithClass(string @class)
        {
            if (_class == null)
                _class = new List<string>();

            _class.Add(@class);
            return this;
        }

        public SirenEntityBuilder WithProperty(string key, object value)
        {
            if (_properties == null)
                _properties = new Dictionary<string, object>();

            _properties[key] = value;
            return this;
        }

        public SirenEntityBuilder WithSubEntity(SubEntityBuilder subEntityBuilder)
        {
            if (_subEntityBuilders == null)
                _subEntityBuilders = new List<SubEntityBuilder>();

            _subEntityBuilders.Add(subEntityBuilder);
            return this;
        }

        public SirenEntityBuilder WithLink(LinkBuilder linkBuilder)
        {
            if (_linkBuilders == null)
                _linkBuilders = new List<LinkBuilder>();

            _linkBuilders.Add(linkBuilder);
            return this;
        }

        public SirenEntityBuilder WithAction(ActionBuilder actionBuilder)
        {
            if (_actionBuilders == null)
                _actionBuilders = new List<ActionBuilder>();

            _actionBuilders.Add(actionBuilder);
            return this;
        }

        public SirenEntityBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public SirenEntity Build()
        {
            SirenEntity entity = new SirenEntity()
            {
                Class = _class?.ToArray(),
                Properties = _properties?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Entities = _subEntityBuilders?.Select(x => x.Build()).ToArray(),
                Links = _linkBuilders?.Select(x => x.Build()).ToArray(),
                Actions = _actionBuilders?.Select(x => x.Build()).ToArray(),
                Title = _title
            };

            if (entity.Actions != null && new HashSet<string>(entity.Actions.Select(x => x.Name)).Count != entity.Actions.Count)
                throw new ArgumentException("Action names MUST be unique within the set of actions for an entity.");

            return entity;
        }
    }
}
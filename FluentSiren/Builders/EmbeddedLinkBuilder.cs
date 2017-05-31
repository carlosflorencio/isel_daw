using System;
using System.Collections.Generic;
using System.Linq;
using FluentSiren.Models;

namespace FluentSiren.Builders
{
    public class EmbeddedLinkBuilder : SubEntityBuilder
    {
        private List<string> _rel;
        private string _href;
        private string _type;

        public EmbeddedLinkBuilder WithRel(string rel)
        {
            if (_rel == null)
                _rel = new List<string>();

            _rel.Add(rel);
            return this;
        }

        public EmbeddedLinkBuilder WithHref(string href)
        {
            _href = href;
            return this;
        }

        public EmbeddedLinkBuilder WithType(string type)
        {
            _type = type;
            return this;
        }

        public override SubEntity Build()
        {
            if (_rel == null || !_rel.Any())
                throw new ArgumentException("Rel is required.");

            if (string.IsNullOrEmpty(_href))
                throw new ArgumentException("Href is required.");

            return new SubEntity
            {
                Class = _class?.ToArray(),
                Rel = _rel?.ToArray(),
                Href = _href,
                Type = _type,
                Title = _title
            };
        }
    }
}
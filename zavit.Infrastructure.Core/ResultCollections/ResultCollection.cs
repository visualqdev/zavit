using System;
using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Infrastructure.Core.ResultCollections
{
    public class ResultCollection<T> : IResultCollection<T>
    {
        readonly List<T> _results;
        
        public ResultCollection(IEnumerable<T> results, int take)
        {
            _results = results.ToList();
            Take = take;
        }
       
        public int Take { get; }

        public bool HasMoreResults => _results.Count > Take;

        public IEnumerable<T> Results => _results.Count > Take ? _results.Take(Take) : _results;
    }
}
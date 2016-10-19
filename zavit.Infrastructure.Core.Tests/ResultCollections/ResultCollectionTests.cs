using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using zavit.Infrastructure.Core.ResultCollections;

namespace zavit.Infrastructure.Core.Tests.ResultCollections 
{
    [Subject("ResultCollection")]
    public class ResultCollectionTests
    {
        class When_checking_if_collection_that_has_more_results_and_the_requested_number_of_results_is_lower_than_number_of_results
        {
            Because of = () => _result = _subject.HasMoreResults;

            It should_return_true = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                var results = new[] {new object(), new object() };

                _subject = new ResultCollection<object>(results, 1);
            };

            static bool _result;
            static ResultCollection<object> _subject;
        }

        class When_checking_if_collection_that_has_more_results_and_the_requested_number_of_results_is_same_as_number_of_results
        {
            Because of = () => _result = _subject.HasMoreResults;

            It should_return_false = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                var results = new[] { new object(), new object() };

                _subject = new ResultCollection<object>(results, 2);
            };

            static bool _result;
            static ResultCollection<object> _subject;
        }

        class When_getting_results_and_there_are_more_results_than_specified_by_take
        {
            Because of = () => _result = _subject.Results.ToList();

            It should_only_return_number_of_items_specified_by_take = () => _result.ShouldContainOnly(_resultItem, _otherResultItem);

            Establish context = () =>
            {
                _resultItem = new object();
                _otherResultItem = new object();
                var extraItem = new object();

                _subject = new ResultCollection<object>(new[] { _resultItem, _otherResultItem, extraItem }, 2);
            };

            static ResultCollection<object> _subject;
            static List<object> _result;
            static object _resultItem;
            static object _otherResultItem;
        }

        class When_getting_results_and_there_same_or_less_results_than_specified_by_take
        {
            Because of = () => _result = _subject.Results.ToList();

            It should_return_number_of_items_specified_by_take = () => _result.ShouldContainOnly(_resultItem, _otherResultItem);

            Establish context = () =>
            {
                _resultItem = new object();
                _otherResultItem = new object();

                _subject = new ResultCollection<object>(new[] { _resultItem, _otherResultItem }, 2);
            };

            static ResultCollection<object> _subject;
            static List<object> _result;
            static object _resultItem;
            static object _otherResultItem;
        }
    }
}


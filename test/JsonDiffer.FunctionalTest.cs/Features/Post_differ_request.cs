using System;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace JsonDiffer.FunctionalTest.Features
{
    [FeatureDescription(
@"In order to compare a JSON document to another posted
As service consumer  
I want to post it.")]
    [Label("Story-1")]
    public partial class Post_differ_request
    {
        [Scenario]
        [MultiAssert]
        public async void Should_compare_equal_sides()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_diff_id("IDTEST"),
                        _ => Given_a_posted_left_side("{\"key\":\"value\"}"),
                        _ => Given_a_posted_right_side("{\"key\":\"value\"}"),
                        _ => When_diff_is_requested(),
                        _ => Then_should_ba_receive_a_ok_message(),
                        _ => Then_should_receive_that_documents_are_equal()
                    );
        }
      
    }
}

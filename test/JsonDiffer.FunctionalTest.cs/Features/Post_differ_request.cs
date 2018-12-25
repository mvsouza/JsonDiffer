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
                        _ => Then_should_receive_a_message_that_they_are_equal()
                    );
        }
        [Scenario]
        [MultiAssert]
        public async void Should_compare_different_sizes_sides()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_diff_id("IDTEST"),
                        _ => Given_a_posted_left_side("{\"key\":\"value\"}"),
                        _ => Given_a_posted_right_side("{\"key\":\"value123\"}"),
                        _ => When_diff_is_requested(),
                        _ => Then_should_ba_receive_a_ok_message(),
                        _ => Then_should_receive_message_that_are_from_different()
                    );
        }
        [Scenario]
        [MultiAssert]
        public async void Should_compare_same_size_jsons_find_different_parts()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_diff_id("IDTEST"),
                        _ => Given_a_posted_left_side("{\"key\":\"value\"}"),
                        _ => Given_a_posted_right_side("{\"key\":\"tests\"}"),
                        _ => When_diff_is_requested(),
                        _ => Then_should_ba_receive_a_ok_message(),
                        _ => Then_should_receive_message_that_are_from_different(),
                        _ => Then_should_specify_the_offset_and_length_of_diff(8,5)
                    );
        }
    }
}

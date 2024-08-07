// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Datasync.Client.Http;

namespace CommunityToolkit.Datasync.Client.Test.Http;

[ExcludeFromCodeCoverage]
public class HttpClientOptions_Tests
{
    [Fact]
    public void Defaults_AreSane()
    {
        HttpClientOptions options = new();
        options.HttpPipeline.Should().BeEmpty();
        options.Timeout.Should().Be(TimeSpan.FromSeconds(60));
        options.UserAgent.Should().StartWith("Datasync/");
    }

    [Fact]
    public void Timeout_CanRoundtrip()
    {
        HttpClientOptions options = new() { Timeout = TimeSpan.FromSeconds(30) };
        options.Timeout.Should().Be(TimeSpan.FromSeconds(30));
    }

    [Fact]
    public void Timeout_ThrowsWhenNeeded()
    {
        HttpClientOptions options = new();
        Action act = () => options.Timeout = TimeSpan.Zero;
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UserAgent_CanRoundtrip()
    {
        HttpClientOptions options = new() { UserAgent = string.Empty };
        options.UserAgent.Should().Be(string.Empty);
        options.UserAgent = "foo";
        options.UserAgent.Should().Be("foo");
        options.UserAgent = "foo ";
        options.UserAgent.Should().Be("foo");
    }
}

﻿using Aranasoft.Cobweb.Extensions;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Tests.Extensions {
    public class GivenAnEnum {
        private enum Fruits {
            [System.ComponentModel.Description("Granny Smith Apples")]
            Apples,
            Oranges,
            Grapes
        }

        [Fact]
        public void ItShouldReturnDescriptionElementsAsDescription() {
            Fruits.Apples.GetDescription().Should().Be("Granny Smith Apples");
        }

        [Fact]
        public void ItShouldReturnNonDescriptionElementsAsEnumName() {
            Fruits.Oranges.GetDescription().Should().Be("Oranges");
        }
    }
}

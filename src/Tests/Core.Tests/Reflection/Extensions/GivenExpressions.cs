using System;
using System.Linq;
using System.Linq.Expressions;
using Cobweb.Reflection.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Reflection.Extensions {
    [TestFixture]
    public class GivenExpressions {
        public class ParamObject {
            public string Prop { get; set; }
        }

        public class ContainerObject {
            public void Run(int arg) {}
            public void Run(int arg1, decimal arg2) {}
            public void Run(ParamObject param) {}
        }

        [Test]
        public void ItShouldIdentifyArgumentCount() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5);

            var arguments = expression.GetMethodArguments();
            arguments.Should().HaveCount(1);
        }

        [Test]
        public void ItShouldIdentifyArgumentCountByMethodValues() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5);

            var arguments = expression.GetMethodArgumentValues();
            arguments.Should().HaveCount(1);
        }


        [Test]
        public void ItShouldMaintainArgumentSort() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5, 5m);

            var arguments = expression.GetMethodArguments();
            arguments.ElementAt(0).Key.Name.Should().Be("arg1");
            arguments.ElementAt(1).Key.Name.Should().Be("arg2");
        }


        [Test]
        public void ItShouldIdentifyArgumentCountForMultipleArgs() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5, 5m);

            var arguments = expression.GetMethodArguments();
            arguments.Should().HaveCount(2);
        }


        [Test]
        public void ItShouldMaintainArgumentSortByMethodValues() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5, 5m);

            var arguments = expression.GetMethodArgumentValues();
            arguments.ElementAt(0).Key.Name.Should().Be("arg1");
            arguments.ElementAt(1).Key.Name.Should().Be("arg2");
        }

        [Test]
        public void ItShouldIdentifyArgumentCountByMethodValuesForMultipleArgs() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5, 5m);

            var arguments = expression.GetMethodArgumentValues();
            arguments.Should().HaveCount(2);
        }


        [Test]
        public void ItShouldIdentifyArgumentValue() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5);

            var arguments = expression.GetMethodArguments();
            arguments.Should().HaveCount(1);
        }

        [Test]
        public void ItShouldIdentifyArgumentValueByMethodValues() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5);

            var arguments = expression.GetMethodArgumentValues();
            arguments.Should().HaveCount(1);
        }


        [Test]
        public void ItShouldIdentifyArgumentValueByConstant() {
            Expression<Action<ContainerObject>> expression = container => container.Run(5);

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.Constant);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = 5;
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }


        [Test]
        public void ItShouldIdentifyArgumentValueByNew() {
            Expression<Action<ContainerObject>> expression = container => container.Run(new ParamObject());

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.New);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject();
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }

        [Test]
        public void ItShouldIdentifyArgumentValueByCall() {
            Func<ParamObject> input = () => new ParamObject();
            Expression<Action<ContainerObject>> expression = container => container.Run(input.Invoke());

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.Call);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject();
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }

        [Test]
        public void ItShouldIdentifyArgumentValueByMemberAccess() {
            var input = new ParamObject();
            Expression<Action<ContainerObject>> expression = container => container.Run(input);

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.MemberAccess);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject();
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }


        [Test]
        public void ItShouldIdentifyArgumentValueByMemberInit() {
            Expression<Action<ContainerObject>> expression = container => container.Run(new ParamObject {Prop = "Foo"});

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.MemberInit);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject {Prop = "Foo"};
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }


        [Test]
        public void ItShouldIdentifyArgumentValueByConditional() {
            ParamObject nullObject = null;
            Expression<Action<ContainerObject>> expression = container =>
                container.Run(nullObject == null ? new ParamObject {Prop = "Foo"} : new ParamObject {Prop = "Bar"});

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.Conditional);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject {Prop = "Foo"};
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }


        [Test]
        public void ItShouldIdentifyArgumentValueByCoalesce() {
            ParamObject nullObject = null;
            Expression<Action<ContainerObject>> expression = container =>
                container.Run(nullObject ?? new ParamObject {Prop = "Bar"});

            expression.GetMethodArguments().First().Value.NodeType.Should().Be(ExpressionType.Coalesce);
            var arguments = expression.GetMethodArgumentValues();
            var expectation = new ParamObject {Prop = "Bar"};
            arguments.Single().Value.ShouldBeEquivalentTo(expectation, options => options.RespectingRuntimeTypes());
        }
    }
}

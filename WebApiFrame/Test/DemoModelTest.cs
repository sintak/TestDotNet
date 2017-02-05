using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrame.Controllers;
using Xunit;

namespace WebApiFrame.Test
{
    public class DemoModelTest
    {
        private readonly DemoModel _demo;

        public DemoModelTest()
        {
            _demo = new DemoModel();
        }

        [Fact]  // [Fact]特性标识表示固定输入的测试用例
        public void AddTest()
        {
            int result = _demo.Add(1, 2);
            Assert.Equal(3, result);
        }

        [Theory]  // [Theory]特性标识表示可以指定多个输入的测试用例，结合InlineData特性标识使用
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void IsOddTest(int num)
        {
            bool result = _demo.IsOdd(num);
            Assert.True(result, $"{num} is not odd");
        }
    }
}

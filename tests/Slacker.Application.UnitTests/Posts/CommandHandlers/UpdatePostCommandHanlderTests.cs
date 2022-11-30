using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.UnitTests.Posts.CommandHandlers;
public class UpdatePostCommandHanlderTests
{
    [Fact]
    public void DemoTest()
    {
        string s = "hello";
        s.ShouldBeOfType(typeof(string));   
    }
}

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace DotNet6API_Sample.Tests.Unit.Helpers;

public class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}
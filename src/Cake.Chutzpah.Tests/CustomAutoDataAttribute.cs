using System;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace Cake.Chutzpah.Tests
{
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute(params Type[] customizationTypes)
            : base(
                new Fixture().Customize(new AutoConfiguredNSubstituteCustomization())
                    .Customize(new CompositeCustomization(
                        customizationTypes.Select(Activator.CreateInstance)
                            .Cast<ICustomization>()
                            .ToArray())))
        {
        }
    }
}
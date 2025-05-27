using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using pspbe.Controllers;

namespace pspbe.Tests
{
    public class RoutingConflictTests
    {
        [Fact]
        public void CustomRoutes_ShouldHaveOrderAttribute_ToAvoidConventionalRouteConflicts()
        {
            // Get all controller types
            var controllerTypes = Assembly.GetAssembly(typeof(CategoryController))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Controller)) && !t.IsAbstract);

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.IsPublic && !m.IsSpecialName && m.DeclaringType == controllerType);

                foreach (var method in methods)
                {
                    var routeAttribute = method.GetCustomAttribute<RouteAttribute>();
                    if (routeAttribute != null && routeAttribute.Template.Contains("{") && routeAttribute.Template.Contains("}"))
                    {
                        // Check if this route could conflict with conventional routes
                        var template = routeAttribute.Template;
                        var segments = template.Split('/');
                        
                        if (segments.Length >= 2 && segments[1].StartsWith("{") && segments[1].EndsWith("}"))
                        {
                            // This is a parameterized route that could conflict with conventional routes
                            // It should have an Order attribute with value > 0
                            Assert.True(routeAttribute.Order > 0, 
                                $"Method {controllerType.Name}.{method.Name} has route '{template}' that could conflict with conventional routes. " +
                                $"Add Order = 2 (or higher) to the [Route] attribute to give conventional routes priority.");
                        }
                    }
                }
            }
        }

        [Fact]
        public void ConventionalRoutes_ShouldNotBeBlockedByCustomRoutes()
        {
            // This test ensures that standard CRUD actions are not blocked by custom routes
            var controllerTypes = Assembly.GetAssembly(typeof(CategoryController))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Controller)) && !t.IsAbstract);

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.IsPublic && !m.IsSpecialName && m.DeclaringType == controllerType);

                var customRoutes = methods
                    .Select(m => new { Method = m, Route = m.GetCustomAttribute<RouteAttribute>() })
                    .Where(x => x.Route != null)
                    .ToList();

                foreach (var customRoute in customRoutes)
                {
                    var template = customRoute.Route.Template;
                    
                    // Check if this route could block conventional routes like /Controller/Create
                    if (template.Contains("{") && template.Contains("}"))
                    {
                        var segments = template.Split('/');
                        if (segments.Length >= 2 && segments[1].StartsWith("{") && segments[1].EndsWith("}"))
                        {
                            // This route pattern could match /Controller/Create, /Controller/Edit etc.
                            // Verify it has proper ordering
                            Assert.True(customRoute.Route.Order > 0,
                                $"Route '{template}' in {controllerType.Name}.{customRoute.Method.Name} " +
                                $"could block conventional routes. Add Order = 2 to the [Route] attribute.");
                        }
                    }
                }
            }
        }
    }
}
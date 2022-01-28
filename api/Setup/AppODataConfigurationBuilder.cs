using data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace api.Setup
{
    public static class AppODataConfigurationBuilder
    {
        public static IEdmModel SetupEdmModel()
        {
            var builder = new ODataConventionModelBuilder { Namespace = "Exp" };

            // Setup data
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Department>("Departments");
            builder.EntitySet<DepartmentLocation>(nameof(DepartmentLocation));

            builder.EntitySet<MembershipUsers>(nameof(MembershipUsers));
            builder.EntitySet<MembershipDepartment>("MembershipDepartments");


            // Pick first entity set if multiple sets with the same type are found
            // This typically represents an error, but it can be very hard to see when the only
            // visible result is that certain navigation properties can't be expanded
            builder.BindingOptions = NavigationPropertyBindingOption.Auto;

            return builder.GetEdmModel();
        }
    }
}

using System.Security.Claims;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class ClassStudentsSirenHto : StudentsSirenHto
    {
        protected override string RouteList => Routes.ClassStudentsList;
        protected int Identifier;

        public ClassStudentsSirenHto(
            IUrlHelperFactory urlHelperFactory, 
            IActionContextAccessor actionContextAccessor) : 
                base(urlHelperFactory, actionContextAccessor)
        {
        }
        

        public SirenEntity WeakCollection(int parentId, PagedList<Student> items, ListQueryStringDto query)
        {
            Identifier = parentId;
            return this.Collection(items, query);
        }

        protected override SirenEntityBuilder AddEntityActions(
            SirenEntityBuilder entity,
            Student item)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("remove-student-from-class")
                            .WithTitle("Remove Student from Class")
                            .WithMethod("DELETE")
                            .WithHref(Url.AbsoluteRouteUrl(
                                Routes.ClassParticipantRemove,
                                new {id = Identifier, studentId = item.Number }
                            ))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionActions(
            SirenEntityBuilder entity)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher))
            {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-student-to-class")
                        .WithTitle("Add Student To Class")
                        .WithMethod("POST")
                        .WithHref(Url.AbsoluteRouteUrl(
                            Routes.ClassParticipantAdd,
                            new {id = Identifier})
                        )
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                            .WithTitle("Number")
                            .WithName("number")
                            .WithType("number")));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(
            SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.AbsoluteRouteUrl(RouteList, new { id = Identifier })))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}
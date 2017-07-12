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
    public class ClassTeachersSirenHto : TeachersSirenHto
    {
        protected override string RouteList { get; } = Routes.ClassTeachersList;

        protected int Identifier;

        public ClassTeachersSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) :
                base(urlHelperFactory, actionContextAccessor)
        {
        }

        public SirenEntity WeakCollection(int parentId, PagedList<Teacher> items, ListQueryStringDto query)
        {
            Identifier = parentId;
            return this.Collection(items, query);
        }


        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Teacher item)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && (c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher)))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("remove-teacher-from-class")
                            .WithTitle("Remove Teacher from Class")
                            .WithMethod("DELETE")
                            .WithHref(Url.AbsoluteRouteUrl(
                                Routes.ClassTeacherRemove,
                                new { id = Identifier, teacherId = item.Number }
                            ))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionActions(SirenEntityBuilder entity)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && (c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher)))
            {
                entity.WithAction(new ActionBuilder()
                            .WithName("add-teacher-to-class")
                            .WithTitle("Add Teacher to Class")
                            .WithMethod("POST")
                            .WithHref(Url.AbsoluteRouteUrl(
                                Routes.ClassTeacherAdd,
                                new { id = Identifier }
                            ))
                            .WithType("application/json")
                            .WithField(new FieldBuilder()
                                .WithTitle("Number")
                                .WithName("number")
                                .WithType("number")));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.AbsoluteRouteUrl(
                        RouteList,
                        new { id = Identifier }
                    )))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}
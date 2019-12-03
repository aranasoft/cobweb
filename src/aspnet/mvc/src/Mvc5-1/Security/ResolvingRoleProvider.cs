using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Cobweb.Web.Mvc.Security {
    public class ResolvingRoleProvider : RoleProvider {
        public override string ApplicationName {
            get { return GetProvider().ApplicationName; }
            set { GetProvider().ApplicationName = value; }
        }

        public virtual IDependencyResolver GetResolver() {
            return DependencyResolver.Current;
        }

        private RoleProvider GetProvider() {
            IDependencyResolver container = GetResolver();
            var provider = container.GetService<RoleProvider>();
            if (provider == null) {
                throw new Exception("Unable to resolve RoleProvider");
            }

            return provider;
        }

        public override bool IsUserInRole(string username, string roleName) {
            return GetProvider().IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username) {
            return GetProvider().GetRolesForUser(username);
        }

        public override void CreateRole(string roleName) {
            GetProvider().CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            return GetProvider().DeleteRole(roleName, throwOnPopulatedRole);
        }

        public override bool RoleExists(string roleName) {
            return GetProvider().RoleExists(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            GetProvider().AddUsersToRoles(usernames, roleNames);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            GetProvider().RemoveUsersFromRoles(usernames, roleNames);
        }

        public override string[] GetUsersInRole(string roleName) {
            return GetProvider().GetUsersInRole(roleName);
        }

        public override string[] GetAllRoles() {
            return GetProvider().GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            return GetProvider().FindUsersInRole(roleName, usernameToMatch);
        }
    }
}

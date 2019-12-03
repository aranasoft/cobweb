using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Cobweb.Web.Mvc.Security {
    public class ResolvingMembershipProvider : MembershipProvider {
        public override bool EnablePasswordRetrieval {
            get { return GetProvider().EnablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset {
            get { return GetProvider().EnablePasswordReset; }
        }

        public override bool RequiresQuestionAndAnswer {
            get { return GetProvider().RequiresQuestionAndAnswer; }
        }

        public override string ApplicationName {
            get { return GetProvider().ApplicationName; }
            set { GetProvider().ApplicationName = value; }
        }

        public override int MaxInvalidPasswordAttempts {
            get { return GetProvider().MaxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow {
            get { return GetProvider().PasswordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail {
            get { return GetProvider().RequiresUniqueEmail; }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get { return GetProvider().PasswordFormat; }
        }

        public override int MinRequiredPasswordLength {
            get { return GetProvider().MinRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get { return GetProvider().MinRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression {
            get { return GetProvider().PasswordStrengthRegularExpression; }
        }

        public virtual IDependencyResolver GetResolver() {
            return DependencyResolver.Current;
        }

        private MembershipProvider GetProvider() {
            IDependencyResolver container = GetResolver();
            var provider = container.GetService<MembershipProvider>();
            if (provider == null) {
                throw new Exception("Unable to resolve MembershipProvider");
            }

            return provider;
        }

        public override MembershipUser CreateUser(string username,
                                                  string password,
                                                  string email,
                                                  string passwordQuestion,
                                                  string passwordAnswer,
                                                  bool isApproved,
                                                  object providerUserKey,
                                                  out MembershipCreateStatus status) {
            return GetProvider()
                .CreateUser(username,
                            password,
                            email,
                            passwordQuestion,
                            passwordAnswer,
                            isApproved,
                            providerUserKey,
                            out status);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username,
                                                             string password,
                                                             string newPasswordQuestion,
                                                             string newPasswordAnswer) {
            return GetProvider()
                .ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordQuestion);
        }

        public override string GetPassword(string username, string answer) {
            return GetProvider().GetPassword(username, answer);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            return GetProvider().ChangePassword(username, oldPassword, newPassword);
        }

        public override string ResetPassword(string username, string answer) {
            return GetProvider().ResetPassword(username, answer);
        }

        public override void UpdateUser(MembershipUser user) {
            GetProvider().UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password) {
            return GetProvider().ValidateUser(username, password);
        }

        public override bool UnlockUser(string userName) {
            return GetProvider().UnlockUser(userName);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            return GetProvider().GetUser(providerUserKey, userIsOnline);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            return GetProvider().GetUser(username, userIsOnline);
        }

        public override string GetUserNameByEmail(string email) {
            return GetProvider().GetUserNameByEmail(email);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            return GetProvider().DeleteUser(username, deleteAllRelatedData);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            return GetProvider().GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline() {
            return GetProvider().GetNumberOfUsersOnline();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch,
                                                                 int pageIndex,
                                                                 int pageSize,
                                                                 out int totalRecords) {
            return GetProvider().FindUsersByName(usernameToMatch, pageIndex, pageIndex, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch,
                                                                  int pageIndex,
                                                                  int pageSize,
                                                                  out int totalRecords) {
            return GetProvider().FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }
    }
}

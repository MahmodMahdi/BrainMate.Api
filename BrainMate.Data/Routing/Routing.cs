namespace BrainMate.Data.Routing
{
	public static class Routing
	{
		public const string SingleRoute = "/{id}";

		public const string root = "Api";
		public const string version = "V1";
		public const string Rule = root + "/" + version + "/";

		public static class RelativesRouting
		{
			public const string Prefix = Rule + "Relatives";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string Paginated = Prefix + "/Paginated";
			public const string Search = Prefix + "/Search";
		}
		public static class PatientRouting
		{
			public const string Prefix = Rule + "Patient";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
		}
		public static class UserRouting
		{
			public const string Prefix = Rule + "User";
			public const string Create = Prefix + "/Create";
			public const string Paginated = Prefix + "/Paginated";
			public const string GetById = Prefix + SingleRoute;
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string ChangePassword = Prefix + "/Change_Password";
		}
		public static class AuthenticationRouting
		{
			public const string prefix = Rule + "Authentication";
			public const string SignIn = prefix + "/SignIn";
			public const string RefreshToken = prefix + "/Refresh-Token";
			public const string ValidateToken = prefix + "/Validate-Token";

			public const string ConfirmEmail = "Api/V1/Authentication/ConfirmEmail";

			public const string SendResetPasswordCode = prefix + "/SendResetPasswordCode";
			public const string ConfirmResetPassword = prefix + "/ConfirmResetPassword";
			public const string ResetPassword = prefix + "/ResetPassword";
		}
		public static class AuthorizationRouting
		{
			public const string prefix = Rule + "Authorization";
			public const string Roles = prefix + "/Roles";
			public const string Claims = prefix + "/Claims";
			public const string Create = Roles + "/Create";
			public const string Update = Roles + "/Update";
			public const string Delete = Roles + "/Delete/{id}";
			public const string GetAll = Roles + "/GetAll";
			public const string GetById = Roles + SingleRoute;

			public const string ManageUserRoles = Roles + "/Manage_UserRoles/{userId}";
			public const string UpdateUserRoles = Roles + "/Update_UserRoles";
			public const string ManageUserClaims = Claims + "/Manage_UserClaims/{userId}";
			public const string UpdateUserClaims = Roles + "/Update_UserClaims";

		}
		public static class EmailRouting
		{
			public const string prefix = Rule + "Email";
			public const string SendEmail = prefix + "/SendEmail";

		}
	}
}

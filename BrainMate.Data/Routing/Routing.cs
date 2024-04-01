namespace BrainMate.Data.Routing
{
	public static class Routing
	{
		public const string SingleRoute = "/{id}";

		public const string root = "Api";
		public const string version = "V1";
		public const string Rule = root + "/" + version + "/";

		public static class PatientRouting
		{
			public const string Prefix = Rule + "Patient";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
		}
		public static class RelativesRouting
		{
			public const string Prefix = Rule + "Relatives";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string Paginated = Prefix + "/Paginated-List";
			public const string Search = Prefix + "/Search";
		}
		public static class MedicineRouting
		{
			public const string Prefix = Rule + "Medicine";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string Paginated = Prefix + "/Paginated-List";
			public const string Search = Prefix + "/Search";
		}
		public static class FoodRouting
		{
			public const string Prefix = Rule + "Food";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string GetAll = Prefix + "/GetAll";
			public const string Search = Prefix + "/Search";
		}
		public static class EventRouting
		{
			public const string Prefix = Rule + "Event";
			public const string GetById = Prefix + SingleRoute;
			public const string Create = Prefix + "/Create";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete/{Id}";
			public const string Paginated = Prefix + "/Paginated";
			public const string Search = Prefix + "/Search";
		}
		public static class UserRouting
		{
			public const string Prefix = Rule + "User";
			public const string Register = Prefix + "/Register";
			public const string CaregiverRegister = Prefix + "/CaregiverRegister";
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
			public const string Logout = prefix + "/Logout";

			public const string ConfirmEmail = "Api/V1/Authentication/ConfirmEmail";

			public const string SendResetPasswordCode = prefix + "/SendResetPasswordCode";
			public const string CaregiverSendResetPassword = prefix + "/CaregiverSendResetPassword";
			public const string ConfirmResetPassword = prefix + "/ConfirmResetPassword";
			public const string ResetPassword = prefix + "/ResetPassword";
		}

		public static class EmailRouting
		{
			public const string prefix = Rule + "Email";
			public const string SendEmail = prefix + "/SendEmail";

		}
	}
}

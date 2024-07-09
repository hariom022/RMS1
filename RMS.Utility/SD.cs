namespace RMS.Utility
{
	public static class SD
	{
		public const string Role_Admin = "Admin";
		public const string Role_Manager = "Manager";
		public const string Role_Employee = "Employee";
		public const string Role_User = "User";

		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}

		public const string StatusPending = "Pending";
		public const string StatusApproved = "Approved";
		public const string StatusInProcess = "Processing";
		public const string StatusCancelled = "Cancelled";

	}
}

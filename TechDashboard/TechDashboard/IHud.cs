using System;
namespace TechDashboard
{
	public interface IHud
	{
		void Show();
		void Show(string status);
		void Dismiss();
	}
}


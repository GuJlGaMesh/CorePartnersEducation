using System;

namespace Meeting_02_04
{
	public delegate void NewMailEventHandler(object sender, NewMailEventArgs e);

	public class NewMail
	{
		public event NewMailEventHandler newMail;
		public string From { get; set; }
		public string To { get; set; }
		public string Text { get; set; }
		public NewMail(string from, string to, string text)
		{
			From = from;
			To = to;
			Text = text;
		}
		public void Start()
		{
			if (!IsEmpty())
			{
				var args = new NewMailEventArgs(this);
				OnNewMail(args);
			}
		}
		public bool IsEmpty() => this.Equals(null);
		public void OnNewMail(NewMailEventArgs e)
		{
			var handler = newMail;
			handler?.Invoke(this, e);
		}

	}

	public class NewMailEventArgs : EventArgs
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Text { get; set; }
		public NewMailEventArgs(string from, string to, string text)
		{
			From = from;
			To = to;
			Text = text;
		}
		public NewMailEventArgs(NewMail nm)
		{
			From = nm.From;
			To = nm.To;
			Text = nm.Text;
		}

	}
}

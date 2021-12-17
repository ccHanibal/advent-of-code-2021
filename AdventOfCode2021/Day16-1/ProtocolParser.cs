namespace Day16_1
{
	public class ProtocolParser
	{
		private readonly IBitsProvider msgProvider;

		public ProtocolParser(IBitsProvider messageProvider)
		{
			this.msgProvider = messageProvider;
		}

		public IPaket ParseMessage()
		{
			var msg = msgProvider.GetBinString();
			var parser = new PaketParser(msg);
			return parser.ParsePaket();
		}
	}
}

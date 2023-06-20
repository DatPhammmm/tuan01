using BusinessObjects;
using System.Messaging;
using System.Windows.Forms;



namespace MSMQ_Receive
{
    public partial class F_MessageReceive : Form
    {
        private MessageQueue queue;
        public F_MessageReceive()
        {
            InitializeComponent();
            init_queue();
        }
        void init_queue()
        {
            string path = @" FormatName:Direct:OS:serverName\private$\queue_nam";
            queue = new MessageQueue(path);
            queue.BeginReceive();
            queue.ReceiveCompleted += Queue_ReceiveCompleted;
        }
        private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = e.Message;
            int type = msg.BodyType;
            XmlMessageFormatter fmt = new XmlMessageFormatter(
            new System.Type[] { typeof(string), typeof(Student) }
            );
            msg.Formatter = fmt;
            var result = msg.Body;
            var t = result.GetType();
            if (t.Equals(typeof(Student)))
            {
                SetText(t + ":" + result);
            }
            else
                SetText("" + result);
            queue.BeginReceive();//loop back
        }
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.MessagesRichTextBox.InvokeRequired)
            {
                SetTextCallback callback = new SetTextCallback(SetText);
                this.Invoke(callback, new object[] { text });
            }
            else
            {
                this.MessagesRichTextBox.AppendText(text + "\n");
            }
        }
    }
}

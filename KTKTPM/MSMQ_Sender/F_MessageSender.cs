using BusinessObjects;
using System;
using System.Messaging;
using System.Windows.Forms;

namespace MSMQ_Sender
{
    public partial class F_MessageSender : Form
    {
        MessageQueue queue = null;
        public F_MessageSender()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            string path = @" FormatName:Direct:OS:serverName\private$\queue_nam";
            //string path = @"hbmnl\private$\phongkehoach";
            if (MessageQueue.Exists(path))
            {
                queue = new MessageQueue(path, QueueAccessMode.Send);
            }
            else
                queue = MessageQueue.Create(path, true);
            queue.Label = "queue cho phong ke hoach";
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            string message = richTextBox1.Text;
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            queue.Send(message, transaction);
            transaction.Commit();
        }
        private void SendObjectButton_Click(object sender, EventArgs e)
        {
            Student st = new Student(1001L, "Nguyễn văn Tèo", new DateTime(1999, 10, 15));
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            queue.Send(st, transaction);
            transaction.Commit();
        }
    }
}

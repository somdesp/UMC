namespace ChatApi
{
    public class EMMessage
    {
        public message modelToEntity(Message msg)
        {
            var msgData = new message();
            msgData.UserID = msg.UserID;
            msgData.Msg = msg.Msg;
            // msgData.MessageID = msg.MessageID;
            return msgData;
        }
    }
}
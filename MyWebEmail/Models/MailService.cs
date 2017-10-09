using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Net.Sockets;
using System.Text;
using LumiSoft.Net;
using LumiSoft.Net.POP3.Client;
using LumiSoft.Net.SMTP.Client;
using LumiSoft.Net.Mail;
using LumiSoft.Net.Mime;
using LumiSoft.Net.MIME;

namespace MyWebEmail.Models
{
    public class MailService
    {
        public List<Mail> GetMails(Stauts status)
        {
            //Stream stream=new MemoryStream()
            List<Mail> mailList = new List<Mail>()
            {
                new Mail("zmm2012@sina.com",new List<string>(){ "zmmtest2017@sina.com"},"测试收件箱","邮箱的body",
                MailPriority.Normal,"Received",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com",new List<string>(){ "zmmtest2017@sina.com"},"测试发件箱","邮件已经发送出去了",
                MailPriority.Normal,"Received",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com",new List<string>(){ "zmmtest2017@sina.com"},"附件测试","这是一个测试附件的邮箱",
                MailPriority.High,"Received",new DateTime(2017,9,24)){ Attachments=new List<Attachment>(){
                    new Attachment(new MemoryStream(), "开题报告.doc", "application/msword"),
                    new Attachment(new MemoryStream(), "初稿.doc", "application/msword"),
                } },
                new Mail("zmm2012@sina.com",new List<string>(){ "zmmtest2017@sina.com"},"test my account","this is an automatic test email, please don't reply",
                MailPriority.Low,"Received",new DateTime(2017,9,25)),
                new Mail("zmm2012@sina.com",new List<string>(){ "zmmtest2017@sina.com"},"是否发送成功","检测邮箱是否发送成功",
                MailPriority.Normal,"Received",new DateTime(2017,9,26)),

                new Mail("zmm2012@sina.com",new List<string>(){ "qusong@cqu.edu.cn"},"开题报告测试","Web mail management system",
                MailPriority.Normal,"Draft",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com",new List<string>(){ "qusong@cqu.edu.cn"},"初稿测试","the 1st thesis, hope teacher to provide the comments",
                MailPriority.Normal,"Draft",new DateTime(2017,9,24)),


                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"scenario 1 test","scenario 1 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"scenario 2 test","scenario 2 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"user accept test 2","UAT 2 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)){ Attachments=new List<Attachment>(){ new Attachment(new MemoryStream(), "初稿.doc", "application/msword") } },
                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"user accept test 1","UAT 1 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"around test","around test body, content should be there.",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),

                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"删除功能测试","邮件的内容",
                MailPriority.Normal,"Trash",new DateTime(2017,9,26)),
                new Mail("zmmtest2017@sina.com",new List<string>(){ "zmm2012@sina.com"},"删除些条邮件","the description of body",
                MailPriority.Normal,"Trash",new DateTime(2017,9,26)),

            };

            return mailList.Where(t => t.Status.Equals(status.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
        }


        public void GetMails()
        {

            TcpClient tcpClient = new TcpClient("", 110);
            using (NetworkStream networkStream = tcpClient.GetStream())
            {
                StreamReader streamReader = new StreamReader(networkStream, Encoding.Default);
                StreamWriter streamWriter = new StreamWriter(networkStream, Encoding.Default);
                streamWriter.AutoFlush = true;
                string str;
                // 读取服务器返回的响应连接信息
                try
                {
                    str = streamReader.ReadLine();

                    if (string.IsNullOrWhiteSpace(str) || str.StartsWith("-ERR"))
                    {
                        throw new Exception("连接失败——服务器没有响应");
                    }
                }
                catch
                {
                    throw;
                }

            }




            //POP3Class

        }


        public List<Mail> ReceiveMail(Setting setting)
        {
            List<Mail> list = new List<Mail>();
            using (POP3_Client pop3_Client = new POP3_Client())
            {
                //设置SMPT服务地址和端口并连接
                pop3_Client.Connect(setting.SmtpHostName, setting.SmtpPort);
                //设置Authentication
                pop3_Client.Auth(new LumiSoft.Net.AUTH.AUTH_SASL_Client_Login(setting.User.UserName, setting.User.Password));
                if (pop3_Client.Messages != null && pop3_Client.Messages.Count > 0)
                {
                    foreach (POP3_ClientMessage message in pop3_Client.Messages)
                    {
                        //将收到的邮件逐一转化Mail实体类型
                        Mail_Message mail_Message = Mail_Message.ParseFromByte(message.MessageToByte());
                        list.Add(new Mail()
                        {
                            From = mail_Message.From.ToString(),
                            To = mail_Message.To.ToArray().Select(address => address.ToString()).ToList(),
                            CreatedDateTime = mail_Message.Date,
                            Subject = mail_Message.Subject,
                            Body = mail_Message.BodyHtmlText,
                            Attachments = mail_Message.Attachments.Select(attach => new Attachment(attach.ContentDisposition.Param_FileName)).ToList()
                        });
                    }
                }
            }
            return list;
        }


        public string SaveMail(Mail mail, Setting setting)
        {

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, setting.User.UserName, "draft");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            };

            string fileName = Path.Combine(path, mail.Subject ?? "主题为空", Guid.NewGuid().ToString());

            if (File.Exists(fileName)) File.Delete(fileName);
            try
            {

                File.WriteAllLines(fileName, new string[] {
                    "subject:",
                    mail.Subject,
                    "body:",
                    mail.Body
                });

                return fileName;
            }
            catch
            {
                throw;
            }
        }

        public void SendEmail(Mail mail, Setting setting)
        {
            Mail_Message message = new Mail_Message()
            {
                From = new Mail_t_MailboxList(),
                Subject = mail.Subject,//增加主题
                Priority = mail.Priority.ToString(),//设置优先级
                MessageID = MIME_Utils.CreateMessageID(),
                Date = mail.CreatedDateTime
            };

            //增加发件人地址
            message.From.Add(new Mail_t_Mailbox(null, mail.From));
            //增加收件人地址
            mail.To.ForEach(address => message.To.Add(new Mail_t_Mailbox(address, address)));

            //增加邮件内容
            MIME_Entity.CreateEntity_Text_Html("Base64", Encoding.Default, mail.Body);

            //增加附件
            if (mail.Attachments != null && mail.Attachments.Count > 0)
            {
                mail.Attachments.ForEach(attachment =>
                {
                    MIME_Entity.CreateEntity_Attachment(attachment.Name);
                });
            }

            using (SMTP_Client smtpClient = new SMTP_Client())
            {
                //设置SMPT服务地址和端口并连接
                smtpClient.Connect(setting.SmtpHostName, setting.SmtpPort);
                //设置Authentication
                smtpClient.Auth(new LumiSoft.Net.AUTH.AUTH_SASL_Client_Login(setting.User.UserName, setting.User.Password));

                using (MemoryStream stream = new MemoryStream())
                {
                    message.ToStream(stream);
                    stream.Position = 0;
                    //发送邮件
                    smtpClient.SendMessage(stream);
                }

            }
        }

        //private void setBody(string mediaType, Mail_Message message, Mail mail) {
        //    //--- multipart/mixed -----------------------------------
        //    MIME_h_ContentType contentType_multipartMixed = new MIME_h_ContentType(MIME_MediaTypes.Multipart.mixed);
        //    contentType_multipartMixed.Param_Boundary = Guid.NewGuid().ToString();
        //    MIME_b_MultipartMixed multipartMixed = new MIME_b_MultipartMixed(contentType_multipartMixed);
        //    message.Body = multipartMixed;

        //    string htmlText = "";//mailInfo.Content;//"<html>这是一份测试邮件，<img src=\"cid:test.jpg\">来自<font color=red><b>LumiSoft.Net</b></font></html>";
        //    MIME_Entity entity_text_html = new MIME_Entity();
        //    MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
        //    entity_text_html.Body = text_html;
        //    text_html.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, htmlText);
        //    multipartMixed.BodyParts.Add(entity_text_html);


        //    //--- multipart/alternative -----------------------------
        //    //MIME_Entity entity_multipartAlternative = new MIME_Entity();
        //    //MIME_h_ContentType contentType_multipartAlternative = new MIME_h_ContentType(MIME_MediaTypes.Multipart.alternative);
        //    //contentType_multipartAlternative.Param_Boundary = Guid.NewGuid().ToString();
        //    //MIME_b_MultipartAlternative multipartAlternative = new MIME_b_MultipartAlternative(contentType_multipartAlternative);
        //    //entity_multipartAlternative.Body = multipartAlternative;
        //    //multipartMixed.BodyParts.Add(entity_multipartAlternative);


        //    //--- text/plain ----------------------------------------
        //    //MIME_Entity entity_text_plain = new MIME_Entity();
        //    //MIME_b_Text text_plain = new MIME_b_Text(MIME_MediaTypes.Text.plain);
        //    //entity_text_plain.Body = text_plain;

        //    ////string plainTextBody = "如果你邮件客户端不支持HTML格式，或者你切换到“普通文本”视图，将看到此内容";
        //    ////if (!string.IsNullOrEmpty(SystemConfig.Default.PlaintTextTips))
        //    ////{
        //    ////    plainTextBody = SystemConfig.Default.PlaintTextTips;
        //    ////}

        //    //text_plain.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, plainTextBody);
        //    //multipartAlternative.BodyParts.Add(entity_text_plain);


        //    //--- text/html -----------------------------------------
        //    string htmlText = "";//mailInfo.Content;//"<html>这是一份测试邮件，<img src=\"cid:test.jpg\">来自<font color=red><b>LumiSoft.Net</b></font></html>";
        //    MIME_Entity entity_text_html = new MIME_Entity();
        //    MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
        //    entity_text_html.Body = text_html;
        //    text_html.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, htmlText);
        //    multipartMixed.BodyParts.Add(entity_text_html);
        //}


    }



    public enum Stauts
    {
        Draft,
        Received,
        Sent,
        Trash
    }
}

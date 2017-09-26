using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace MyWebEmail.Models
{
    public class MailService
    {
        public List<Mail> GetMails(Stauts status)
        {
            //Stream stream=new MemoryStream()
            List<Mail> mailList = new List<Mail>()
            {
                new Mail("zmm2012@sina.com.cn",new List<string>(){ "zmmtest2017@sina.com.cn"},"测试收件箱","邮箱的body",
                MailPriority.Normal,"Received",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com.cn",new List<string>(){ "zmmtest2017@sina.com.cn"},"测试发件箱","邮件已经发送出去了",
                MailPriority.Normal,"Received",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com.cn",new List<string>(){ "zmmtest2017@sina.com.cn"},"附件测试","这是一个测试附件的邮箱",
                MailPriority.High,"Received",new DateTime(2017,9,24)){ Attachments=new List<Attachment>(){
                    new Attachment(new MemoryStream(), "开题报告.doc", "application/msword"),
                    new Attachment(new MemoryStream(), "初稿.doc", "application/msword"),
                } },
                new Mail("zmm2012@sina.com.cn",new List<string>(){ "zmmtest2017@sina.com.cn"},"test my account","this is an automatic test email, please don't reply",
                MailPriority.Low,"Received",new DateTime(2017,9,25)),
                new Mail("zmm2012@sina.com.cn",new List<string>(){ "zmmtest2017@sina.com.cn"},"是否发送成功","检测邮箱是否发送成功",
                MailPriority.Normal,"Received",new DateTime(2017,9,26)),

                new Mail("zmm2012@sina.com",new List<string>(){ "qusong@cqu.edu.cn"},"开题报告测试","Web mail management system",
                MailPriority.Normal,"Draft",new DateTime(2017,9,24)),
                new Mail("zmm2012@sina.com",new List<string>(){ "qusong@cqu.edu.cn"},"初稿测试","the 1st thesis, hope teacher to provide the comments",
                MailPriority.Normal,"Draft",new DateTime(2017,9,24)),


                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"scenario 1 test","scenario 1 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"scenario 2 test","scenario 2 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"user accept test 2","UAT 2 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)){ Attachments=new List<Attachment>(){ new Attachment(new MemoryStream(), "初稿.doc", "application/msword") } },
                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"user accept test 1","UAT 1 body",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),
                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"around test","around test body, content should be there.",
                MailPriority.Normal,"Sent",new DateTime(2017,9,27)),

                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"删除功能测试","邮件的内容",
                MailPriority.Normal,"Trash",new DateTime(2017,9,26)),
                new Mail("zmmtest2017@sina.com.cn",new List<string>(){ "zmm2012@sina.com"},"删除些条邮件","the description of body",
                MailPriority.Normal,"Trash",new DateTime(2017,9,26)),

            };

            return mailList.Where(t => t.Status.Equals(status.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }

    public enum Stauts
    {
        Draft,
        Received,
        Sent,
        Trash
    }
}